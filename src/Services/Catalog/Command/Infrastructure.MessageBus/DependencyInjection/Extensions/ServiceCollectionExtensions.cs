using System.Reflection;
using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using Contracts.JsonConverters;
using FluentValidation;
using Infrastructure.MessageBus.DependencyInjection.Options;
using Infrastructure.MessageBus.PipeFilters;
using Infrastructure.MessageBus.PipeObservers;
using MassTransit;
using MassTransit.Configuration;
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
                cfg.AddConsumers(Assembly.GetExecutingAssembly());

                cfg.UsingRabbitMq((context, bus) =>
                {
                    var options = context.GetRequiredService<IOptionsMonitor<MessageBusOptions>>().CurrentValue;

                    bus.Host(
                        hostAddress: options.ConnectionString,
                        connectionName: $"{options.ConnectionName}.{AppDomain.CurrentDomain.FriendlyName}");

                    cfg.AddMessageScheduler(new($"queue:{options.SchedulerQueueName}"));

                    bus.UseInMemoryScheduler(
                        schedulerFactory: context.GetRequiredService<ISchedulerFactory>(),
                        queueName: options.SchedulerQueueName);

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

                    // TODO - Solve this!
                    // bus.UseConsumeFilter(typeof(BusinessValidatorFilter<>), context);

                    bus.UseConsumeFilter(typeof(ContractValidatorFilter<>), context);
                    bus.ConnectReceiveObserver(new LoggingReceiveObserver());
                    bus.ConnectConsumeObserver(new LoggingConsumeObserver());
                    bus.ConnectPublishObserver(new LoggingPublishObserver());
                    bus.ConnectSendObserver(new LoggingSendObserver());
                    bus.ConfigureEventReceiveEndpoints(context);
                    bus.ConfigureEndpoints(context);

                    bus.ConfigurePublish(pipe => pipe.AddPipeSpecification(
                        new DelegatePipeSpecification<PublishContext<IEvent>>(ctx
                            => ctx.CorrelationId = ctx.InitiatorId)));
                });
            })
            .AddQuartz();

    public static IServiceCollection AddEventBusGateway(this IServiceCollection services)
        => services.AddScoped<IEventBusGateway, EventBusGateway>();

    public static IServiceCollection AddMessageValidators(this IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining(typeof(IMessage));

    public static OptionsBuilder<MessageBusOptions> ConfigureMessageBusOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<MessageBusOptions>()
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