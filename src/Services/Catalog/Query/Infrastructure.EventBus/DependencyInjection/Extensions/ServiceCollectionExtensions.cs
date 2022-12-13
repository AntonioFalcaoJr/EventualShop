using System.Reflection;
using Contracts.Abstractions.Messages;
using Contracts.JsonConverters;
using FluentValidation;
using Infrastructure.EventBus.DependencyInjection.Options;
using Infrastructure.EventBus.PipeFilters;
using Infrastructure.EventBus.PipeObservers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventBus(this IServiceCollection services)
        => services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();
            cfg.AddConsumers(Assembly.GetExecutingAssembly());

            cfg.UsingRabbitMq((context, bus) =>
            {
                var options = context.GetRequiredService<IOptionsMonitor<EventBusOptions>>().CurrentValue;

                bus.Host(options.ConnectionString);

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
                bus.ConnectReceiveObserver(new LoggingReceiveObserver());
                bus.ConnectConsumeObserver(new LoggingConsumeObserver());
                bus.ConfigureEventReceiveEndpoints(context);
                bus.ConfigureEndpoints(context);
            });
        });

    public static IServiceCollection AddMessageValidators(this IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining(typeof(IMessage));

    public static OptionsBuilder<EventBusOptions> ConfigureEventBusOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<EventBusOptions>()
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