using System.Reflection;
using Application.DependencyInjection;
using GrpcService;
using Infrastructure.EventBus.DependencyInjection.Extensions;
using Infrastructure.EventBus.DependencyInjection.Options;
using Infrastructure.Projections.DependencyInjection;
using MassTransit;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((context, provider) =>
{
    provider.ValidateScopes =
        provider.ValidateOnBuild =
            context.HostingEnvironment.IsDevelopment();
});

builder.Configuration
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .AddEnvironmentVariables();

builder.Logging.ClearProviders().AddSerilog();

builder.Host.UseSerilog((context, cfg)
    => cfg.ReadFrom.Configuration(context.Configuration));

builder.Host.ConfigureServices((context, services) =>
{
    services.AddCors(options
        => options.AddDefaultPolicy(policyBuilder
            => policyBuilder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));

    if (context.HostingEnvironment.IsEnvironment("Testing"))
        services.AddTestingEventBus();
    else services.AddEventBus();

    services.AddGrpc();
    services.AddMessageValidators();
    services.AddProjections();
    services.AddInteractors();

    services.ConfigureEventBusOptions(
        context.Configuration.GetSection(nameof(EventBusOptions)));

    services.ConfigureMassTransitHostOptions(
        context.Configuration.GetSection(nameof(MassTransitHostOptions)));

    services.AddHttpLogging(options
        => options.LoggingFields = HttpLoggingFields.All);
});

var app = builder.Build();

app.UseCors();
app.UseSerilogRequestLogging();
app.MapGrpcService<ShoppingCartGrpcService>();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}

namespace GrpcService
{
    public partial class Program;
}