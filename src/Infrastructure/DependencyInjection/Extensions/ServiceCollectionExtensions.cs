using System;
using System.Reflection;
using Application.Abstractions.UseCases;
using Application.EventSourcing.Customers.EventStore;
using Application.EventSourcing.Customers.Projections;
using Application.UseCases.Customers.Commands.DeleteCustomer;
using Application.UseCases.Customers.Commands.RegisterCustomer;
using Application.UseCases.Customers.Commands.UpdateCustomer;
using Application.UseCases.Customers.EventHandlers.CustomerDeleted;
using Application.UseCases.Customers.EventHandlers.CustomerRegistered;
using Application.UseCases.Customers.EventHandlers.CustomerUpdated;
using Domain.Abstractions.Events;
using Domain.Entities.Customers;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.EventSourcing.Customers.EventStore;
using Infrastructure.EventSourcing.Customers.EventStore.Contexts;
using Infrastructure.EventSourcing.Customers.Projections;
using Infrastructure.EventSourcing.Customers.Projections.Contexts;
using MassTransit;
using MassTransit.Definition;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;
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
        public static IServiceCollection AddMassTransit(this IServiceCollection services)
            => services.AddMassTransit(cfg =>
                {
                    cfg.AddConsumers(
                        filter: type => type.IsAssignableTo(typeof(IConsumer)),
                        assemblies: typeof(ICommand).Assembly);

                    cfg.SetKebabCaseEndpointNameFormatter();
                    cfg.AddCommandEndpointsConvention();

                    cfg.UsingRabbitMq((context, bus) =>
                    {
                        bus.Host(
                            host: "192.168.100.9",
                            configure: host =>
                            {
                                host.Username("guest");
                                host.Password("guest");
                            });

                        bus.ConfigureEventReceiveEndpoints(context);
                        bus.ConfigureEndpoints(context);
                    });
                })
                .AddGenericRequestClient()
                .AddMassTransitHostedService();

        private static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration context)
        {
            cfg.ConfigureEventReceiveEndpoint<Events.CustomerRegistered, CustomerRegisteredConsumer>(context);
            cfg.ConfigureEventReceiveEndpoint<Events.CustomerAgeChanged, CustomerUpdatedConsumer>(context);
            cfg.ConfigureEventReceiveEndpoint<Events.CustomerNameChanged, CustomerUpdatedConsumer>(context);
            cfg.ConfigureEventReceiveEndpoint<Events.CustomerDeleted, CustomerDeletedConsumer>(context);
        }

        private static void AddCommandEndpointsConvention(this IServiceCollectionBusConfigurator _)
        {
            MapQueueEndpoint<RegisterCustomer>();
            MapQueueEndpoint<UpdateCustomer>();
            MapQueueEndpoint<DeleteCustomer>();
        }

        private static void ConfigureEventReceiveEndpoint<TMessage, TConsumer>(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
            where TMessage : IDomainEvent
            where TConsumer : class, IConsumer
            => cfg.ReceiveEndpoint(
                queueName: typeof(TMessage).ToKebabCaseString(),
                configureEndpoint: endpoint => endpoint.ConfigureConsumer<TConsumer>(registration));

        private static void MapQueueEndpoint<TMessage>()
            where TMessage : class
            => EndpointConvention.Map<TMessage>(new Uri($"queue:{typeof(TMessage).ToKebabCaseString()}"));

        private static string ToKebabCaseString(this MemberInfo member)
            => KebabCaseEndpointNameFormatter.Instance.SanitizeName(member.Name);

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
                .AddScoped<ICustomerEventStoreService, CustomerEventStoreService>()
                .AddScoped<ICustomerProjectionsService, CustomerProjectionsService>();

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
            => services.AddScoped<ICustomerEventStoreRepository, CustomerEventStoreRepository>();

        public static IServiceCollection AddProjectionsRepositories(this IServiceCollection services)
            => services.AddScoped<ICustomerProjectionsRepository, CustomerProjectionsRepository>();

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
}