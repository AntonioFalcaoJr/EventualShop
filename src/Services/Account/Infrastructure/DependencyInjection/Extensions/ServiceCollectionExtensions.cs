using System;
using System.Reflection;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Application.UseCases.CommandHandlers;
using Application.UseCases.EventHandlers;
using Application.UseCases.EventHandlers.Projections;
using Application.UseCases.QueriesHandlers;
using FluentValidation;
using GreenPipes;
using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;
using Infrastructure.DependencyInjection.Filters;
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
using Messages.Abstractions.Commands;
using Messages.Abstractions.Events;
using Messages.Accounts;
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
                        bus.UseConsumeFilter(typeof(MessageValidatorFilter<>), context);
                        bus.ConfigureEventReceiveEndpoints(context);
                        bus.ConfigureEndpoints(context);
                    });
                })
                .AddMassTransitHostedService()
                .AddGenericRequestClient();

        private static void AddCommandConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddCommandConsumer<CreateAccountConsumer, Commands.CreateAccount>();
            cfg.AddCommandConsumer<DefineProfessionalAddressConsumer, Commands.DefineProfessionalAddress>();
            cfg.AddCommandConsumer<DefineResidenceAddressConsumer, Commands.DefineResidenceAddress>();
            cfg.AddCommandConsumer<DeleteAccountConsumer, Commands.DeleteAccount>();
            cfg.AddCommandConsumer<UpdateProfileConsumer, Commands.UpdateProfile>();
        }

        private static void AddEventConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<AccountCreatedConsumer>();
            cfg.AddConsumer<AccountDeletedConsumer>();
            cfg.AddConsumer<ProfessionalAddressDefinedConsumer>();
            cfg.AddConsumer<ProfileUpdatedConsumer>();
            cfg.AddConsumer<ResidenceAddressDefinedConsumer>();
            cfg.AddConsumer<UserRegisteredConsumer>();
        }

        private static void AddQueryConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<GetAccountDetailsConsumer>();
            cfg.AddConsumer<GetAccountsDetailsWithPaginationConsumer>();
        }

        private static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
        {
            cfg.ConfigureEventReceiveEndpoint<AccountCreatedConsumer, Events.AccountCreated>(registration);
            cfg.ConfigureEventReceiveEndpoint<AccountDeletedConsumer, Events.AccountDeleted>(registration);
            cfg.ConfigureEventReceiveEndpoint<ProfessionalAddressDefinedConsumer, Events.ProfessionalAddressDefined>(registration);
            cfg.ConfigureEventReceiveEndpoint<ProfileUpdatedConsumer, Events.ProfileUpdated>(registration);
            cfg.ConfigureEventReceiveEndpoint<ResidenceAddressDefinedConsumer, Events.ResidenceAddressDefined>(registration);
            cfg.ConfigureEventReceiveEndpoint<UserRegisteredConsumer, Messages.Identities.Events.UserRegistered>(registration);
        }

        private static void AddCommandConsumer<TConsumer, TCommand>(this IRegistrationConfigurator configurator)
            where TConsumer : class, IConsumer
            where TCommand : class, ICommand
        {
            configurator
                .AddConsumer<TConsumer>()
                .Endpoint(endpoint => endpoint.ConfigureConsumeTopology = false);

            MapQueueEndpoint<TCommand>();
        }

        private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
            where TConsumer : class, IConsumer
            where TEvent : class, IEvent
        {
            bus.ReceiveEndpoint(
                queueName: $"account-{typeof(TEvent).ToKebabCaseString()}",
                configureEndpoint: endpoint =>
                {
                    endpoint.ConfigureConsumeTopology = false;

                    endpoint.ConfigureConsumer<TConsumer>(registration);
                    endpoint.Bind<TEvent>();

                    endpoint.UseCircuitBreaker(circuitBreaker => // TODO - Options
                    {
                        circuitBreaker.TripThreshold = 15;
                        circuitBreaker.ResetInterval = TimeSpan.FromMinutes(3);
                        circuitBreaker.TrackingPeriod = TimeSpan.FromMinutes(1);
                        circuitBreaker.ActiveThreshold = 10;
                    });

                    endpoint.UseRateLimit(100, TimeSpan.FromSeconds(1)); // TODO - Options
                });
        }

        private static void MapQueueEndpoint<TMessage>()
            where TMessage : class, IMessage
            => EndpointConvention.Map<TMessage>(new Uri($"exchange:{typeof(TMessage).ToKebabCaseString()}"));

        internal static string ToKebabCaseString(this MemberInfo member)
            => KebabCaseEndpointNameFormatter.Instance.SanitizeName(member.Name);

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
                .AddScoped<IAccountEventStoreService, AccountEventStoreService>()
                .AddScoped<IAccountProjectionsService, AccountProjectionsService>();

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
            => services.AddScoped<IAccountEventStoreRepository, AccountEventStoreRepository>();

        public static IServiceCollection AddProjectionsRepositories(this IServiceCollection services)
            => services.AddScoped<IAccountProjectionsRepository, AccountProjectionsRepository>();
        
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