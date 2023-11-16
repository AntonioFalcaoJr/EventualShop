using System.Reflection;
using Application.DependencyInjection;
using GrpcService;
using Infrastructure.Authentication.DependencyInjection.Extensions;
using Infrastructure.Authentication.DependencyInjection.Options;
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

    services.AddGrpc();
    services.AddEventBus();
    services.AddEventInteractors();
    services.AddQueryInteractors();
    services.AddMessageValidators();
    services.AddProjections();
    services.AddJwtAuthentication();

    services.ConfigureEventBusOptions(
        context.Configuration.GetSection(nameof(EventBusOptions)));

    services.ConfigureJwtOptions(
        context.Configuration.GetSection(nameof(JwtOptions)));

    services.ConfigureMassTransitHostOptions(
        context.Configuration.GetSection(nameof(MassTransitHostOptions)));

    services.AddHttpLogging(options
        => options.LoggingFields = HttpLoggingFields.All);
});

var app = builder.Build();

app.UseCors();
app.UseSerilogRequestLogging();
app.MapGrpcService<IdentityGrpcService>();

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