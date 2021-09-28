using System;
using System.Reflection;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Application.UseCases.CommandHandlers;
using Application.UseCases.EventHandlers;
using Application.UseCases.QueriesHandlers;
using Application.UseCases.Validators;
using FluentValidation;
using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;
using Infrastructure.EventSourcing.Projections;
using Infrastructure.EventSourcing.Projections.Contexts;
using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using MassTransit.Topology;
using Messages.Abstractions;
using Messages.Abstractions.Events;
using Messages.Identities;
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
            cfg.AddCommandConsumer<RegisterUserConsumer, Commands.RegisterUser>();
            cfg.AddCommandConsumer<ChangeUserPasswordConsumer, Commands.ChangeUserPassword>();
            cfg.AddCommandConsumer<DeleteUserConsumer, Commands.DeleteUser>();
        }

        private static void AddEventConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<UserChangedConsumer>();
        }

        private static void AddQueryConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<GetUserAuthenticationDetailsConsumer>();
        }

        private static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
        {
            cfg.ConfigureEventReceiveEndpoint<UserChangedConsumer, Events.UserRegistered>(registration);
            //cfg.ConfigureEventReceiveEndpoint<UserChangedConsumer, Events.UserPasswordChanged>(registration);
            //cfg.ConfigureEventReceiveEndpoint<UserChangedConsumer, Events.UserDeleted>(registration);
        }

        private static void AddCommandConsumer<TConsumer, TMessage>(this IRegistrationConfigurator configurator)
            where TConsumer : class, IConsumer
            where TMessage : class
        {
            configurator
                .AddConsumer<TConsumer>()
                .Endpoint(endpoint =>
                {
                    endpoint.ConfigureConsumeTopology = false;
                   // endpoint.UseMyFilter();
                });

            MapQueueEndpoint<TMessage>();
        }

        private static void ConfigureEventReceiveEndpoint<TConsumer, TMessage>(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
            where TConsumer : class, IConsumer
            where TMessage : class, IEvent
        {
            cfg.ReceiveEndpoint(
                queueName: $"identity-{typeof(TMessage).ToKebabCaseString()}",
                configureEndpoint: endpoint =>
                {
                    endpoint.UseConsumeFilter(typeof(MessageValidatorFilter<>), registration);
                    endpoint.ConfigureConsumeTopology = false;
                    endpoint.ConfigureConsumer<TConsumer>(registration);
                    endpoint.Bind<TMessage>();
                });
        }

        private static void MapQueueEndpoint<TMessage>()
            where TMessage : class
            => EndpointConvention.Map<TMessage>(new Uri($"exchange:{typeof(TMessage).ToKebabCaseString()}"));

        internal static string ToKebabCaseString(this MemberInfo member) 
            => KebabCaseEndpointNameFormatter.Instance.SanitizeName(member.Name);

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
                .AddScoped<IUserEventStoreService, UserEventStoreService>()
                .AddScoped<IUserProjectionsService, UserProjectionsService>();

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
            => services.AddScoped<IUserEventStoreRepository, UserEventStoreRepository>();

        public static IServiceCollection AddProjectionsRepositories(this IServiceCollection services)
            => services.AddScoped<IUserProjectionsRepository, UserProjectionsRepository>();

        public static IServiceCollection AddMessageFluentValidation(this IServiceCollection services)
            => services.AddValidatorsFromAssemblyContaining(typeof(IMessage));

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