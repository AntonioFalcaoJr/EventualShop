using System;
using System.Reflection;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Application.UseCases.Commands;
using Application.UseCases.EventHandlers;
using Application.UseCases.Queries;
using Domain.Abstractions.Events;
using Domain.Entities.Catalogs;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.EventSourcing.Catalogs.EventStore;
using Infrastructure.EventSourcing.Catalogs.EventStore.Contexts;
using Infrastructure.EventSourcing.Catalogs.Projections;
using Infrastructure.EventSourcing.Catalogs.Projections.Contexts;
using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using MassTransit.Topology;
using Messages.Catalogs.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly RabbitMqOptions RabbitMqOptions = new();

        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services, Action<RabbitMqOptions> optionsAction)
            => services.AddMassTransit(cfg =>
                {
                    optionsAction(RabbitMqOptions);

                    cfg.SetKebabCaseEndpointNameFormatter();

                    cfg.AddCommandConsumers();
                    cfg.AddEventConsumers();
                    cfg.AddQueryConsumers();

                    cfg.UsingRabbitMq((context, bus) =>
                    {
                        bus.Host(
                            host: RabbitMqOptions.Host,
                            // virtualHost: RabbitMqOptions.VirtualHost,
                            host =>
                            {
                                host.Username(RabbitMqOptions.Username);
                                host.Password(RabbitMqOptions.Password);
                            });

                        bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());
                        bus.ConfigureEventReceiveEndpoints(context);
                        bus.ConfigureEndpoints(context);
                    });
                })
                .AddMassTransitHostedService()
                .AddGenericRequestClient();

        private static void AddCommandConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddCommandConsumer<DeleteCatalogConsumer, DeleteCatalog>();
            cfg.AddCommandConsumer<UpdateCatalogConsumer, UpdateCatalog>();
            cfg.AddCommandConsumer<CreateCatalogConsumer, CreateCatalog>();
            cfg.AddCommandConsumer<ActivateCatalogConsumer, ActivateCatalog>();
            cfg.AddCommandConsumer<DeactivateCatalogConsumer, DeactivateCatalog>();
            cfg.AddCommandConsumer<AddCatalogItemConsumer, AddCatalogItem>();
            cfg.AddCommandConsumer<RemoveCatalogItemConsumer, RemoveCatalogItem>();
        }

        private static void AddEventConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<CatalogCreatedConsumer>();
            cfg.AddConsumer<CatalogDeletedConsumer>();
            cfg.AddConsumer<CatalogActivatedConsumer>();
            cfg.AddConsumer<CatalogDeactivatedConsumer>();
            cfg.AddConsumer<CatalogChangedConsumer>();
            cfg.AddConsumer<CatalogChangedConsumer>();
        }

        private static void AddQueryConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<GetCatalogItemsDetailsConsumer>();
            cfg.AddConsumer<GetAccountsDetailsWithPaginationConsumer>();
        }

        private static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
        {
            cfg.ConfigureEventReceiveEndpoint<CatalogCreatedConsumer, Events.CatalogCreated>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogDeletedConsumer, Events.CatalogDeleted>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogActivatedConsumer, Events.CatalogActivated>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogDeactivatedConsumer, Events.CatalogDeactivated>(registration);
            
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogUpdated>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogItemAdded>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogItemRemoved>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogItemEdited>(registration);
        }

        private static void AddCommandConsumer<TConsumer, TMessage>(this IRegistrationConfigurator configurator)
            where TConsumer : class, IConsumer
            where TMessage : class
        {
            configurator
                .AddConsumer<TConsumer>()
                .Endpoint(e => e.ConfigureConsumeTopology = false);

            MapQueueEndpoint<TMessage>();
        }

        private static void ConfigureEventReceiveEndpoint<TConsumer, TMessage>(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
            where TConsumer : class, IConsumer
            where TMessage : class, IDomainEvent
        {
            cfg.ReceiveEndpoint(
                queueName: typeof(TMessage).ToKebabCaseString(),
                configureEndpoint: endpoint =>
                {
                    endpoint.ConfigureConsumer<TConsumer>(registration);
                    endpoint.ConfigureConsumeTopology = false;
                });
        }

        private static void MapQueueEndpoint<TMessage>()
            where TMessage : class
            => EndpointConvention.Map<TMessage>(new Uri($"exchange:{typeof(TMessage).ToKebabCaseString()}"));

        internal static string ToKebabCaseString(this MemberInfo member)
            => KebabCaseEndpointNameFormatter.Instance.SanitizeName(member.Name);

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
                .AddScoped<ICatalogEventStoreService, CatalogEventStoreService>()
                .AddScoped<ICatalogProjectionsService, CatalogProjectionsService>();

        public static IServiceCollection AddEventStoreDbContext(this IServiceCollection services)
            => services
                .AddScoped<DbContext, EventStoreDbContext>()
                .AddDbContext<EventStoreDbContext>();

        public static IServiceCollection AddProjectionsDbContext(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
            return services.AddScoped<IMongoDbContext, MongoDbContext>();
        }

        public static IServiceCollection AddEventStoreRepositories(this IServiceCollection services)
            => services.AddScoped<ICatalogEventStoreRepository, CatalogEventStoreRepository>();

        public static IServiceCollection AddProjectionsRepositories(this IServiceCollection services)
            => services.AddScoped<ICatalogProjectionsRepository, CatalogProjectionsRepository>();

        public static OptionsBuilder<SqlServerRetryingOptions> ConfigureSqlServerRetryingOptions(this IServiceCollection services, IConfigurationSection section)
            => services
                .AddOptions<SqlServerRetryingOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart();

        public static OptionsBuilder<MongoDbOptions> ConfigureMongoDbOptions(this IServiceCollection services, IConfigurationSection section)
            => services
                .AddOptions<MongoDbOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart();

        public static OptionsBuilder<EventStoreOptions> ConfigureEventStoreOptions(this IServiceCollection services, IConfigurationSection section)
            => services
                .AddOptions<EventStoreOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart();
    }

    internal class KebabCaseEntityNameFormatter : IEntityNameFormatter
    {
        public string FormatEntityName<T>()
            => typeof(T).ToKebabCaseString();
    }
}