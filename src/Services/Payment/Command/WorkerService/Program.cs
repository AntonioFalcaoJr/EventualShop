using Application.DependencyInjection.Extensions;
using Infrastructure.EventStore.Contexts;
using Infrastructure.EventStore.DependencyInjection.Extensions;
using Infrastructure.EventStore.DependencyInjection.Options;
using Infrastructure.HTTP.DependencyInjection.Extensions;
using Infrastructure.HTTP.DependencyInjection.Options;
using Infrastructure.MessageBus.DependencyInjection.Extensions;
using Infrastructure.MessageBus.DependencyInjection.Options;
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

builder.ConfigureLogging((context, logging) =>
{
    Log.Logger = new LoggerConfiguration().ReadFrom
        .Configuration(context.Configuration)
        .CreateLogger();

    logging.ClearProviders();
    logging.AddSerilog();
});

builder.ConfigureServices((context, services) =>
{
    services.AddEventStore();
    services.AddMessageBus();
    services.AddEventBusGateway();
    services.AddApplicationServices();
    services.AddCommandInteractors();
    services.AddEventInteractors();
    services.AddMessageValidators();

    services.AddPaymentGateway();
    services.AddCreditCardHttpClient();
    services.AddPayPalHttpClient();
    services.AddDebitCardHttpClient();

    services.ConfigureEventStoreOptions(
        context.Configuration.GetSection(nameof(EventStoreOptions)));

    services.ConfigureSqlServerRetryOptions(
        context.Configuration.GetSection(nameof(SqlServerRetryOptions)));

    services.ConfigureMessageBusOptions(
        context.Configuration.GetSection(nameof(MessageBusOptions)));
    
    services.ConfigureMassTransitHostOptions(
        context.Configuration.GetSection(nameof(MassTransitHostOptions)));
    
    services.ConfigureQuartzOptions(
        context.Configuration.GetSection(nameof(QuartzOptions)));
    
    services.ConfigurePayPalHttpClientOptions(
        context.Configuration.GetSection(nameof(PayPalHttpClientOptions)));
    
    services.ConfigureDebitCardHttpClientOptions(
        context.Configuration.GetSection(nameof(DebitCardHttpClientOptions)));
    
    services.ConfigureCreditCardHttpClientOptions(
        context.Configuration.GetSection(nameof(CreditCardHttpClientOptions)));
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