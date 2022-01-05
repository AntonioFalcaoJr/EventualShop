using System;
using Application.UseCases.Events;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Catalog;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogCreated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogDeleted>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogActivated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogDeactivated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogUpdated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogItemAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogItemRemoved>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogItemUpdated>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"catalog.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
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