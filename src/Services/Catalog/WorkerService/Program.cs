using System;
using System.Reflection;
using Infrastructure.DependencyInjection.Extensions;
using Infrastructure.DependencyInjection.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = Host.CreateDefaultBuilder(args);

builder.UseDefaultServiceProvider((context, options) =>
{
    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
    options.ValidateOnBuild = true;
});

builder.ConfigureAppConfiguration(configurationBuilder =>
{
    configurationBuilder
        .AddUserSecrets(Assembly.GetExecutingAssembly())
        .AddEnvironmentVariables();
});

builder.ConfigureLogging((context, loggingBuilder) =>
{
    Log.Logger = new LoggerConfiguration().ReadFrom
        .Configuration(context.Configuration)
        .CreateLogger();

    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});

builder.UseSerilog();

builder.ConfigureServices((context, services) =>
{
    services.AddApplicationServices();

    services.AddEventStoreRepositories();
    services.AddProjectionsRepositories();

    services.AddEventStoreDbContext();
    services.AddProjectionsDbContext();

    services.AddMassTransitWithRabbitMq(options
        => context.Configuration.Bind(nameof(RabbitMqOptions), options));

    services.ConfigureEventStoreOptions(
        context.Configuration.GetSection(nameof(EventStoreOptions)));

    services.ConfigureMongoDbOptions(
        context.Configuration.GetSection(nameof(MongoDbOptions)));

    services.ConfigureSqlServerRetryingOptions(
        context.Configuration.GetSection(nameof(SqlServerRetryingOptions)));
});

var host = builder.Build();

try
{
    await using var scope = host.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
    await dbContext.Database.MigrateAsync();
    await dbContext.Database.EnsureCreatedAsync();
    await host.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
}
finally
{
    Log.CloseAndFlush();
}