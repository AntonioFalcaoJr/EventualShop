using System;
using Application.UseCases.EventHandlers;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Messages.Abstractions.Events;
using Messages.Catalogs;

namespace Infrastructure.DependencyInjection.Extensions
{
    internal static class RabbitMqBusConfiguratorExtensions
    {
        public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
        {
            cfg.ConfigureEventReceiveEndpoint<CatalogCreatedConsumer, Events.CatalogCreated>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogDeleted>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogActivated>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogDeactivated>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogUpdated>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogItemAdded>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogItemRemoved>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogItemUpdated>(registration);
        }

        private static void ConfigureEventReceiveEndpoint<TConsumer, TMessage>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
            where TConsumer : class, IConsumer
            where TMessage : class, IEvent
        {
            bus.ReceiveEndpoint(
                queueName: $"catalog-{typeof(TMessage).ToKebabCaseString()}",
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