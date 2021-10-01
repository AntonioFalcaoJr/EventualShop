using System;
using Application.UseCases.EventHandlers;
using Application.UseCases.EventHandlers.Projections;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Messages.Abstractions.Events;
using Events = Messages.Accounts.Events;

namespace Infrastructure.DependencyInjection.Extensions
{
    internal static class RabbitMqBusFactoryConfiguratorExtensions
    {
        public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
        {
            cfg.ConfigureEventReceiveEndpoint<AccountCreatedConsumer, Events.AccountCreated>(registration);
            cfg.ConfigureEventReceiveEndpoint<AccountDeletedConsumer, Events.AccountDeleted>(registration);
            cfg.ConfigureEventReceiveEndpoint<ProfessionalAddressDefinedConsumer, Events.ProfessionalAddressDefined>(registration);
            cfg.ConfigureEventReceiveEndpoint<ProfileUpdatedConsumer, Events.ProfileUpdated>(registration);
            cfg.ConfigureEventReceiveEndpoint<ResidenceAddressDefinedConsumer, Events.ResidenceAddressDefined>(registration);
            cfg.ConfigureEventReceiveEndpoint<UserRegisteredConsumer, Messages.Identities.Events.UserRegistered>(registration);
        }

        private static void ConfigureEventReceiveEndpoint<TConsumer, TMessage>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
            where TConsumer : class, IConsumer
            where TMessage : class, IEvent
        {
            bus.ReceiveEndpoint(
                queueName: $"account-{typeof(TMessage).ToKebabCaseString()}",
                configureEndpoint: endpoint =>
                {
                    endpoint.ConfigureConsumeTopology = false;

                    endpoint.ConfigureConsumer<TConsumer>(registration);
                    endpoint.Bind<TMessage>();

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
    }
}