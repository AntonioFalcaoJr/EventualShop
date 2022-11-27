using Contracts.JsonConverters;
using Contracts.Services.Account.Grpc;
using Contracts.Services.Identity.Grpc;
using Grpc.Core;
using MassTransit;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebAPI.DependencyInjection.Options;
using WebAPI.PipeObservers;

namespace WebAPI.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services)
        => services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();

            cfg.UsingRabbitMq((context, bus) =>
            {
                var options = context.GetRequiredService<IOptionsMonitor<MessageBusOptions>>().CurrentValue;

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

                bus.ConnectReceiveObserver(new LoggingReceiveObserver());
                bus.ConnectConsumeObserver(new LoggingConsumeObserver());
                bus.ConnectSendObserver(new LoggingSendObserver());
                bus.ConfigureEndpoints(context);
            });
        });

    public static void AddIdentityGrpcClient(this IServiceCollection services)
        => services.AddGrpcClient<IdentityService.IdentityServiceClient, IdentityGrpcClientOptions>();

    public static void AddAccountGrpcClient(this IServiceCollection services)
        => services.AddGrpcClient<AccountService.AccountServiceClient, AccountGrpcClientOptions>();

    private static void AddGrpcClient<TClient, TOptions>(this IServiceCollection services)
        where TClient : ClientBase
        where TOptions : class
        => services.AddGrpcClient<TClient>((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptionsMonitor<TOptions>>().CurrentValue as dynamic;
                client.Address = new(options.BaseAddress);
            })
            .EnableCallContextPropagation(options
                => options.SuppressContextNotFoundErrors = true);

    public static OptionsBuilder<MessageBusOptions> ConfigureMessageBusOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<MessageBusOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<MassTransitHostOptions> ConfigureMassTransitHostOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<MassTransitHostOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<RabbitMqTransportOptions> ConfigureRabbitMqTransportOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<RabbitMqTransportOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<IdentityGrpcClientOptions> ConfigureIdentityGrpcClientOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<IdentityGrpcClientOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}