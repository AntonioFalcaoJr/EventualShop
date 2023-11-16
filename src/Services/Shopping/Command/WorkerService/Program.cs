using Application.DependencyInjection.Extensions;
using Infrastructure.EventBus.DependencyInjection.Extensions;
using Infrastructure.EventStore.DependencyInjection.Extensions;
using Serilog;
using WorkerService.Extensions;

using var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServiceProvider()
    .ConfigureAppConfiguration()
    .ConfigureLogging()
    .ConfigureServices(services
        => services
            .AddApplication()
            .AddMessageBusInfrastructure()
            .AddEventStoreInfrastructure())
    .Build();

try
{
    var environment = host.Services.GetRequiredService<IHostEnvironment>();

    if (environment.IsDevelopment() || environment.IsStaging())
        await host.MigrateEventStoreAsync();

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