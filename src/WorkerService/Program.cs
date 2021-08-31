using Infrastructure.DependencyInjection.Extensions;
using Infrastructure.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WorkerService;

var host = Host.CreateDefaultBuilder(args);

host.UseDefaultServiceProvider((context, options) =>
{
    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
    options.ValidateOnBuild = true;
});

host.ConfigureLogging((context, builder) =>
{
    Log.Logger = new LoggerConfiguration().ReadFrom
        .Configuration(context.Configuration)
        .CreateLogger();

    builder.AddSerilog();
});

host.UseSerilog();

host.ConfigureServices((context, services) =>
{
    services.AddHostedService<WorkerService.Worker>();

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

await host.Build().RunAsync();