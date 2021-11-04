using System;
using Application.UseCases.Events;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Messages.Abstractions.Events;
using Messages.Services.Catalogs;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
    {
        cfg.ConfigureEventReceiveEndpoint<CatalogCreatedConsumer, DomainEvents.CatalogCreated>(registration);
        cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, DomainEvents.CatalogDeleted>(registration);
        cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, DomainEvents.CatalogActivated>(registration);
        cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, DomainEvents.CatalogDeactivated>(registration);
        cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, DomainEvents.CatalogUpdated>(registration);
        cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, DomainEvents.CatalogItemAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, DomainEvents.CatalogItemRemoved>(registration);
        cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, DomainEvents.CatalogItemUpdated>(registration);
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