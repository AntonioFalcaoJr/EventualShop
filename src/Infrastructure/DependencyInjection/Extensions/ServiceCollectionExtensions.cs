using System;
using System.Reflection;
using Application.EventSourcing.Customers.EventStore;
using Application.EventSourcing.Customers.Projections;
using Application.UseCases.Customers.Commands.DeleteCustomer;
using Application.UseCases.Customers.Commands.RegisterCustomer;
using Application.UseCases.Customers.Commands.UpdateCustomer;
using Application.UseCases.Customers.EventHandlers.CustomerDeleted;
using Application.UseCases.Customers.EventHandlers.CustomerRegistered;
using Application.UseCases.Customers.EventHandlers.CustomerUpdated;
using Application.UseCases.Customers.Queries.GetCustomerDetails;
using Application.UseCases.Customers.Queries.GetCustomersWithPagination;
using Domain.Abstractions.Events;
using Domain.Entities.Customers;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.EventSourcing.Customers.EventStore;
using Infrastructure.EventSourcing.Customers.EventStore.Contexts;
using Infrastructure.EventSourcing.Customers.Projections;
using Infrastructure.EventSourcing.Customers.Projections.Contexts;
using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
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
            cfg.AddCommandConsumer<DeleteCustomerConsumer, DeleteCustomer>();
            cfg.AddCommandConsumer<UpdateCustomerConsumer, UpdateCustomer>();
            cfg.AddCommandConsumer<RegisterCustomerConsumer, RegisterCustomer>();
        }

        private static void AddEventConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<CustomerRegisteredConsumer>();
            cfg.AddConsumer<CustomerDeletedConsumer>();
            cfg.AddConsumer<CustomerUpdatedConsumer>();
        }

        private static void AddQueryConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<GetCustomerDetailsQueryConsumer>();
            cfg.AddConsumer<GetCustomersDetailsWithPaginationQueryConsumer>();
        }

        private static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
        {
            cfg.ConfigureEventReceiveEndpoint<CustomerRegisteredConsumer, Events.CustomerRegistered>(registration);
            cfg.ConfigureEventReceiveEndpoint<CustomerUpdatedConsumer, Events.CustomerAgeChanged>(registration);
            cfg.ConfigureEventReceiveEndpoint<CustomerUpdatedConsumer, Events.CustomerNameChanged>(registration);
            cfg.ConfigureEventReceiveEndpoint<CustomerDeletedConsumer, Events.CustomerDeleted>(registration);
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

    internal class KebabCaseEntityNameFormatter : IEntityNameFormatter
    {
        public string FormatEntityName<T>()
            => typeof(T).ToKebabCaseString();
    }
}