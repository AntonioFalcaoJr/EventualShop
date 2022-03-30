using System.Reflection;
using Application.Abstractions.Notifications;
using ECommerce.Abstractions.Messages;
using ECommerce.JsonConverters;
using FluentValidation;
using Infrastructure.MessageBus.DependencyInjection.Options;
using Infrastructure.MessageBus.DependencyInjection.PipeFilters;
using Infrastructure.MessageBus.DependencyInjection.PipeObservers;
using Infrastructure.MessageBus.Notifications;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services)
        => services.AddMassTransit(cfg =>
            {
                cfg.SetKebabCaseEndpointNameFormatter();
                cfg.AddConsumers(Assembly.Load(nameof(Application)));

                cfg.UsingRabbitMq((context, bus) =>
                {
                    var options = context.GetRequiredService<IOptionsMonitor<RabbitMqOptions>>().CurrentValue;

                    bus.Host(
                        host: options.Host,
                        port: options.Port,
                        virtualHost: options.VirtualHost,
                        configure: host =>
                        {
                            host.Username(options.Username);
                            host.Password(options.Password);

                            options.Cluster?.ForEach(node
                                => host.UseCluster(cluster => cluster.Node(node)));
                        });

                    cfg.AddMessageScheduler(new Uri($"queue:{options.SchedulerQueueName}"));

                    bus.UseInMemoryScheduler(schedulerCfg =>
                    {
                        schedulerCfg.QueueName = options.SchedulerQueueName;
                        schedulerCfg.SchedulerFactory = context.GetRequiredService<ISchedulerFactory>();
                    });

                    bus.UseMessageRetry(retry
                        => retry.Incremental(
                            retryLimit: options.RetryLimit,
                            initialInterval: options.InitialInterval,
                            intervalIncrement: options.IntervalIncrement));

                    bus.UseNewtonsoftJsonSerializer();

                    bus.ConfigureNewtonsoftJsonSerializer(settings =>
                    {
                        settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                        settings.Converters.Add(new DateOnlyJsonConverter());
                        settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                        return settings;
                    });

                    bus.ConfigureNewtonsoftJsonDeserializer(settings =>
                    {
                        settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                        settings.Converters.Add(new DateOnlyJsonConverter());
                        settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                        return settings;
                    });

                    bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());
                    bus.UseConsumeFilter(typeof(ContractValidatorFilter<>), context);
                    bus.UseConsumeFilter(typeof(BusinessValidatorFilter<>), context);
                    bus.ConnectReceiveObserver(new LoggingReceiveObserver());
                    bus.ConnectConsumeObserver(new LoggingConsumeObserver());
                    bus.ConnectPublishObserver(new LoggingPublishObserver());
                    bus.ConnectSendObserver(new LoggingSendObserver());
                    bus.ConfigureEventReceiveEndpoints(context);
                    bus.ConfigureEndpoints(context);
                });
            })
            .AddQuartz();

    public static IServiceCollection AddMessageValidators(this IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining(typeof(IMessage));

    public static IServiceCollection AddNotificationContext(this IServiceCollection services)
        => services.AddScoped<INotificationContext, NotificationContext>();

    public static OptionsBuilder<RabbitMqOptions> ConfigureRabbitMqOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<RabbitMqOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<QuartzOptions> ConfigureQuartzOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<QuartzOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<MassTransitHostOptions> ConfigureMassTransitHostOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<MassTransitHostOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}