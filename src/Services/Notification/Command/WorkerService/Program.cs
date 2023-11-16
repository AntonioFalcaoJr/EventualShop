using Application.DependencyInjection.Extensions;
using Infrastructure.EventBus.DependencyInjection.Extensions;
using Infrastructure.EventBus.DependencyInjection.Options;
using Infrastructure.EventStore.Contexts;
using Infrastructure.EventStore.DependencyInjection.Extensions;
using Infrastructure.EventStore.DependencyInjection.Options;
using Infrastructure.SMTP.DependencyInjection.Extensions;
using Infrastructure.SMTP.DependencyInjection.Options;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;

var builder = Host.CreateDefaultBuilder(args);

builder.UseDefaultServiceProvider((context, provider) =>
{
    provider.ValidateScopes =
        provider.ValidateOnBuild =
            context.HostingEnvironment.IsDevelopment();
});

builder.ConfigureAppConfiguration(configuration =>
{
    configuration
        .AddUserSecrets<Program>()
        .AddEnvironmentVariables();
});

builder.ConfigureLogging(logging 
    => logging.ClearProviders().AddSerilog());

builder.UseSerilog((context, cfg) 
    => cfg.ReadFrom.Configuration(context.Configuration));

builder.ConfigureServices((context, services) =>
{
    services.AddEventStore();
    services.AddMessageBus();
    services.AddEventBusGateway();
    services.AddApplicationServices();
    services.AddEventInteractors();
    services.AddMessageValidators();
    services.AddEmailGateway();
    services.AddSmtpClient();
    services.AddLazyFactory();

    services.ConfigureEventStoreOptions(
        context.Configuration.GetSection(nameof(EventStoreOptions)));

    services.ConfigureSqlServerRetryOptions(
        context.Configuration.GetSection(nameof(SqlServerRetryOptions)));

    services.ConfigureEventBusOptions(
        context.Configuration.GetSection(nameof(EventBusOptions)));

    services.ConfigureQuartzOptions(
        context.Configuration.GetSection(nameof(QuartzOptions)));

    services.ConfigureMassTransitHostOptions(
        context.Configuration.GetSection(nameof(MassTransitHostOptions)));
    
    services.ConfigureSmtpOptions(
        context.Configuration.GetSection(nameof(SmtpOptions)));
});

using var host = builder.Build();

try
{
    var environment = host.Services.GetRequiredService<IHostEnvironment>();

    if (environment.IsDevelopment() || environment.IsStaging())
    {
        await using var scope = host.Services.CreateAsyncScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<EventStoreDbContext>();
        await dbContext.Database.MigrateAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }

    await host.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await host.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    host.Dispose();
}