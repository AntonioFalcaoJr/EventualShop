using System.Security.Authentication;
using ECommerce.JsonConverters;
using ECommerce.WebAPI.DependencyInjection.Options;
using ECommerce.WebAPI.DependencyInjection.PipeObservers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ECommerce.WebAPI.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        => services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();

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
                        host.UseSsl(ssl => ssl.Protocol = SslProtocols.Tls12);

                        options.Cluster?.ForEach(node
                            => host.UseCluster(cluster => cluster.Node(node)));
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
                bus.ConnectConsumeObserver(new LoggingConsumeObserver());
                bus.ConnectSendObserver(new LoggingSendObserver());
                bus.ConfigureEndpoints(context);
            });
        });

    public static OptionsBuilder<RabbitMqOptions> ConfigureRabbitMqOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<RabbitMqOptions>()
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