using System;
using System.Reflection;
using Application.Abstractions.Notifications;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Application.Services;
using Application.Services.CreditCards;
using Application.Services.CreditCards.Http;
using Application.Services.DebitCards;
using Application.Services.DebitCards.Http;
using Application.Services.PayPal;
using Application.Services.PayPal.Http;
using ECommerce.Abstractions.Messages;
using ECommerce.JsonConverters;
using FluentValidation;
using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;
using Infrastructure.DependencyInjection.HttpPolicies;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.DependencyInjection.PipeFilters;
using Infrastructure.DependencyInjection.PipeObservers;
using Infrastructure.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;
using Infrastructure.EventSourcing.Projections;
using Infrastructure.EventSourcing.Projections.Contexts;
using Infrastructure.Notifications;
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
using Polly;
using Quartz;
using Quartz.Spi;

namespace Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMassTransitWithRabbitMqAndQuartz(this IServiceCollection services)
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

                    cfg.AddMessageScheduler(new Uri($"queue:{options.SchedulerQueueName}"));

                    bus.UseInMemoryScheduler(schedulerCfg =>
                    {
                        schedulerCfg.QueueName = options.SchedulerQueueName;
                        schedulerCfg.SchedulerFactory = context.GetRequiredService<ISchedulerFactory>();
                        schedulerCfg.JobFactory = context.GetRequiredService<IJobFactory>();
                        schedulerCfg.StartScheduler = true;
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
                    bus.ConnectConsumeObserver(new LoggingConsumeObserver());
                    bus.ConnectPublishObserver(new LoggingPublishObserver());
                    bus.ConnectSendObserver(new LoggingSendObserver());
                    bus.ConfigureEventReceiveEndpoints(context);
                    bus.ConfigureEndpoints(context);
                });
            })
            .AddQuartz();

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
                client.Timeout = options.OverallTimeout;
            })
            .AddPolicyHandler((provider, _) =>
            {
                var options = provider.GetRequiredService<IOptionsMonitor<PayPalHttpClientOptions>>().CurrentValue;

                return Policy.WrapAsync(
                    HttpPolicy.GetRetryPolicyAsync(options.RetryCount, options.SleepDurationPower, options.EachRetryTimeout),
                    HttpPolicy.GetCircuitBreakerPolicyAsync(options.CircuitBreaking, options.DurationOfBreak));
            });

    public static IHttpClientBuilder AddCreditCardHttpClient(this IServiceCollection services)
        => services
            .AddHttpClient<ICreditCardHttpClient, CreditCardHttpClient>()
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptionsMonitor<CreditCardHttpClientOptions>>().CurrentValue;
                client.BaseAddress = new(options.BaseAddress);
                client.Timeout = options.OverallTimeout;
            })
            .AddPolicyHandler((provider, _) =>
            {
                var options = provider.GetRequiredService<IOptionsMonitor<CreditCardHttpClientOptions>>().CurrentValue;

                return Policy.WrapAsync(
                    HttpPolicy.GetRetryPolicyAsync(options.RetryCount, options.SleepDurationPower, options.EachRetryTimeout),
                    HttpPolicy.GetCircuitBreakerPolicyAsync(options.CircuitBreaking, options.DurationOfBreak));
            });

    public static IHttpClientBuilder AddDebitCardHttpClient(this IServiceCollection services)
        => services
            .AddHttpClient<IDebitCardHttpClient, DebitCardHttpClient>()
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<DebitCardHttpClientOptions>>().Value;
                client.BaseAddress = new(options.BaseAddress);
                client.Timeout = options.OverallTimeout;
            })
            .AddPolicyHandler((provider, _) =>
            {
                var options = provider.GetRequiredService<IOptionsMonitor<DebitCardHttpClientOptions>>().CurrentValue;

                return Policy.WrapAsync(
                    HttpPolicy.GetRetryPolicyAsync(options.RetryCount, options.SleepDurationPower, options.EachRetryTimeout),
                    HttpPolicy.GetCircuitBreakerPolicyAsync(options.CircuitBreaking, options.DurationOfBreak));
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

    public static IServiceCollection AddNotificationContext(this IServiceCollection services)
        => services.AddScoped<INotificationContext, NotificationContext>();

    public static OptionsBuilder<SqlServerRetryOptions> ConfigureSqlServerRetryOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<SqlServerRetryOptions>()
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