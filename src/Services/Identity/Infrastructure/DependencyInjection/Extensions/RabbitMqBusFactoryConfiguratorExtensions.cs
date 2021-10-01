using System;
using Application.UseCases.EventHandlers;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Messages.Abstractions.Events;
using Messages.Identities;

namespace Infrastructure.DependencyInjection.Extensions
{
    internal static class RabbitMqBusFactoryConfiguratorExtensions
    {
        public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
        {
            cfg.ConfigureEventReceiveEndpoint<UserChangedConsumer, Events.UserRegistered>(registration);
            cfg.ConfigureEventReceiveEndpoint<UserChangedConsumer, Events.UserPasswordChanged>(registration);
            cfg.ConfigureEventReceiveEndpoint<UserChangedConsumer, Events.UserDeleted>(registration);
        }

        private static void ConfigureEventReceiveEndpoint<TConsumer, TMessage>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
            where TConsumer : class, IConsumer
            where TMessage : class, IEvent
        {
            bus.ReceiveEndpoint(
                queueName: $"identity-{typeof(TMessage).ToKebabCaseString()}",
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