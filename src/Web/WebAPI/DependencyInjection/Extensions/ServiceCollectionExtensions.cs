using Contracts.Abstractions.Messages;
using Contracts.JsonConverters;
using Contracts.Services.Account.Protobuf;
using Contracts.Services.Catalog.Protobuf;
using Contracts.Services.Communication.Protobuf;
using Contracts.Services.Identity.Protobuf;
using Contracts.Services.ShoppingCart.Protobuf;
using Contracts.Services.Warehouse.Protobuf;
using Grpc.Core;
using Grpc.Net.Client.Configuration;
using MassTransit;
using MassTransit.Configuration;
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
                
                bus.ConfigureSend(pipe => pipe.AddPipeSpecification(
                    new DelegatePipeSpecification<SendContext<ICommand>>(sendContext =>
                    {
                        sendContext.CorrelationId = Guid.NewGuid();
                    })));
            });
        });

    public static void AddIdentityGrpcClient(this IServiceCollection services)
        => services.AddGrpcClient<IdentityService.IdentityServiceClient, IdentityGrpcClientOptions>();

    public static void AddAccountGrpcClient(this IServiceCollection services)
        => services.AddGrpcClient<AccountService.AccountServiceClient, AccountGrpcClientOptions>();

    public static void AddCommunicationGrpcClient(this IServiceCollection services)
        => services.AddGrpcClient<CommunicationService.CommunicationServiceClient, CommunicationGrpcClientOptions>();

    public static void AddCatalogGrpcClient(this IServiceCollection services)
        => services.AddGrpcClient<CatalogService.CatalogServiceClient, CatalogGrpcClientOptions>();

    public static void AddWarehouseGrpcClient(this IServiceCollection services)
        => services.AddGrpcClient<WarehouseService.WarehouseServiceClient, WarehouseGrpcClientOptions>();
    
    public static void AddShoppingCartGrpcClient(this IServiceCollection services)
        => services.AddGrpcClient<ShoppingCartService.ShoppingCartServiceClient, ShoppingCartGrpcClientOptions>();

    private static void AddGrpcClient<TClient, TOptions>(this IServiceCollection services)
        where TClient : ClientBase
        where TOptions : class
        => services.AddGrpcClient<TClient>((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptionsMonitor<TOptions>>().CurrentValue as dynamic;
                client.Address = new(options.BaseAddress);
            })
            .ConfigureChannel(options =>
                {
                    options.Credentials = ChannelCredentials.Insecure;
                    options.ServiceConfig = new() { LoadBalancingConfigs = { new RoundRobinConfig() } };
                }
            )
            .ConfigurePrimaryHttpMessageHandler(() =>
                new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                    EnableMultipleHttp2Connections = true
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

    public static OptionsBuilder<AccountGrpcClientOptions> ConfigureAccountGrpcClientOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<AccountGrpcClientOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<CommunicationGrpcClientOptions> ConfigureCommunicationGrpcClientOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<CommunicationGrpcClientOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<CatalogGrpcClientOptions> ConfigureCatalogGrpcClientOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<CatalogGrpcClientOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<WarehouseGrpcClientOptions> ConfigureWarehouseGrpcClientOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<WarehouseGrpcClientOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<ShoppingCartGrpcClientOptions> ConfigureShoppingCartGrpcClientOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<ShoppingCartGrpcClientOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}