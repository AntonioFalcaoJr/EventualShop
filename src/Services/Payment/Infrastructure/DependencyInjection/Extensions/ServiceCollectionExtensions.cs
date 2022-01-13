using System.Linq;
using System.Reflection;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Application.Services;
using Application.Services.CreditCards;
using Application.Services.CreditCards.Http;
using Application.Services.DebitCards;
using Application.Services.DebitCards.Http;
using Application.Services.PayPal;
using Application.Services.PayPal.Http;
using ECommerce.Abstractions;
using ECommerce.JsonConverters;
using FluentValidation;
using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;
using Infrastructure.DependencyInjection.Filters;
using Infrastructure.DependencyInjection.Observers;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;
using Infrastructure.EventSourcing.Projections;
using Infrastructure.EventSourcing.Projections.Contexts;
using Infrastructure.Services.CreditCards;
using Infrastructure.Services.DebitCards;
using Infrastructure.Services.PayPal;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;

namespace Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        => services.AddMassTransit(cfg =>
            {
                cfg.SetKebabCaseEndpointNameFormatter();
                cfg.AddConsumers(Assembly.Load(nameof(Application)));

                cfg.UsingRabbitMq((context, bus) =>
                {
                    var options = context
                        .GetRequiredService<IOptions<RabbitMqOptions>>()
                        .Value;

                    bus.Host(
                        host: options.Host,
                        port: options.Port,
                        virtualHost: options.VirtualHost,
                        host =>
                        {
                            host.Username(options.Username);
                            host.Password(options.Password);
                        });

                    bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());
                    bus.UseConsumeFilter(typeof(MessageValidatorFilter<>), context);
                    bus.ConnectConsumeObserver(new LoggingConsumeObserver());
                    bus.ConnectPublishObserver(new LoggingPublishObserver());
                    bus.ConnectSendObserver(new LoggingSendObserver());
                    bus.ConfigureEventReceiveEndpoints(context);
                    bus.ConfigureEndpoints(context);

                    bus.ConfigureJsonSerializer(settings =>
                    {
                        settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                        settings.Converters.Add(new DateOnlyJsonConverter());
                        settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                        return settings;
                    });

                    bus.ConfigureJsonDeserializer(settings =>
                    {
                        settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                        settings.Converters.Add(new DateOnlyJsonConverter());
                        settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                        return settings;
                    });
                });
            })
            .AddMassTransitHostedService();

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddScoped<IPaymentEventStoreService, PaymentEventStoreService>()
            .AddScoped<IPaymentProjectionsService, PaymentProjectionsService>()
            .AddScoped<IPayPalPaymentService, PayPalPaymentService>()
            .AddScoped<ICreditCardPaymentService, CreditCardPaymentService>()
            .AddScoped<IDebitCardPaymentService, DebitCardPaymentService>()
            .AddScoped<IPaymentStrategy, PaymentStrategy>();

    public static IHttpClientBuilder AddPayPalHttpClient(this IServiceCollection services)
        => services
            .AddHttpClient<IPayPalHttpClient, PayPalHttpClient>()
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<PayPalHttpClientOptions>>().Value;
                client.BaseAddress = new(options.BaseAddress);
            });

    public static IHttpClientBuilder AddCreditCardHttpClient(this IServiceCollection services)
        => services
            .AddHttpClient<ICreditCardHttpClient, CreditCardHttpClient>()
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<CreditCardHttpClientOptions>>().Value;
                client.BaseAddress = new(options.BaseAddress);
            });

    public static IHttpClientBuilder AddDebitCardHttpClient(this IServiceCollection services)
        => services
            .AddHttpClient<IDebitCardHttpClient, DebitCardHttpClient>()
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<DebitCardHttpClientOptions>>().Value;
                client.BaseAddress = new(options.BaseAddress);
            });

    public static IServiceCollection AddEventStoreDbContext(this IServiceCollection services)
        => services
            .AddScoped<DbContext, EventStoreDbContext>()
            .AddDbContext<EventStoreDbContext>();

    public static IServiceCollection AddProjectionsDbContext(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
        return services.AddScoped<IMongoDbContext, ProjectionsDbContext>();
    }

    public static IServiceCollection AddEventStoreRepository(this IServiceCollection services)
        => services.AddScoped<IPaymentEventStoreRepository, PaymentEventStoreRepository>();

    public static IServiceCollection AddProjectionsRepository(this IServiceCollection services)
        => services.AddScoped<IPaymentProjectionsRepository, PaymentProjectionsRepository>();

    public static IServiceCollection AddMessagesFluentValidation(this IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining(typeof(IMessage));

    public static OptionsBuilder<SqlServerRetryingOptions> ConfigureSqlServerRetryingOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<SqlServerRetryingOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<EventStoreOptions> ConfigureEventStoreOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<EventStoreOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<RabbitMqOptions> ConfigureRabbitMqOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<RabbitMqOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<PayPalHttpClientOptions> ConfigurePayPalHttpClientOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<PayPalHttpClientOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<CreditCardHttpClientOptions> ConfigureCreditCardHttpClientOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<CreditCardHttpClientOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    public static OptionsBuilder<DebitCardHttpClientOptions> ConfigureDebitCardHttpClientOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<DebitCardHttpClientOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}