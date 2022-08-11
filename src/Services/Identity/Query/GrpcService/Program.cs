using System.Diagnostics;
using System.Reflection;
using Application.DependencyInjection.Extensions;
using GrpcService;
using Infrastructure.Authentication.DependencyInjection.Extensions;
using Infrastructure.Authentication.DependencyInjection.Options;
using Infrastructure.MessageBus.DependencyInjection.Extensions;
using Infrastructure.MessageBus.DependencyInjection.Options;
using Infrastructure.Projections.DependencyInjection.Extensions;
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

builder.Host.ConfigureAppConfiguration(configuration =>
{
    configuration
        .AddUserSecrets(Assembly.GetExecutingAssembly())
        .AddEnvironmentVariables();
});

builder.Host.ConfigureLogging((context, logging) =>
{
    Log.Logger = new LoggerConfiguration().ReadFrom
        .Configuration(context.Configuration)
        .CreateLogger();

    logging.ClearProviders();
    logging.AddSerilog();
    builder.Host.UseSerilog();
});

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
    services.AddMessageValidators();
    services.AddQueryInteractors();
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

var applicationLifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

applicationLifetime.ApplicationStopping.Register(() =>
{
    Log.Information("Waiting 20s for a graceful termination...");
    Thread.Sleep(20000);
});

applicationLifetime.ApplicationStopped.Register(() =>
{
    Log.Information("Application completely stopped");
    Process.GetCurrentProcess().Kill();
});

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