using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;
using Infrastructure.EventSourcing.Projections;
using Infrastructure.EventSourcing.Projections.Contexts;
using MassTransit;
using MassTransit.Topology;
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
            => services.AddMassTransit(bus =>
                {
                    optionsAction(RabbitMqOptions);

                    bus.SetKebabCaseEndpointNameFormatter();

                    bus.AddCommandConsumers();
                    bus.AddEventConsumers();
                    bus.AddQueryConsumers();

                    bus.UsingRabbitMq((context, rabbit) =>
                    {
                        rabbit.Host(
                            host: RabbitMqOptions.Host,
                            // virtualHost: RabbitMqOptions.VirtualHost,
                            host =>
                            {
                                host.Username(RabbitMqOptions.Username);
                                host.Password(RabbitMqOptions.Password);
                            });

                        rabbit.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());
                        rabbit.ConfigureEventReceiveEndpoints(context);
                        rabbit.ConfigureEndpoints(context);
                    });
                })
                .AddMassTransitHostedService()
                .AddGenericRequestClient();

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
            return services.AddScoped<IMongoDbContext, ProjectionsDbContext>();
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