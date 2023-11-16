using Application.DependencyInjection.Extensions;
using CorrelationId;
using GrpcService;
using GrpcService.Extensions;
using Infrastructure.EventBus.DependencyInjection.Extensions;
using Infrastructure.EventStore.DependencyInjection.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder
    .ConfigureLogging()
    .ConfigureServiceProvider()
    .ConfigureAppConfiguration()
    .ConfigureServices(services =>
    {
        // TODO: Improve this
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        services.AddGrpc();

        services
            .AddProblemDetails()
            .AddCorrelationId();

        services
            .AddApplication()
            .AddMessageBusInfrastructure()
            .AddEventStoreInfrastructure();
    });

var app = builder.Build();

app.UseCorrelationId();
app.UseSerilogRequestLogging();

app.MapGrpcService<ShoppingCartGrpcCommandService>();
app.MapHealthChecks("/healthz").ShortCircuit();

try
{
    if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
        await app.MigrateEventStoreAsync();

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