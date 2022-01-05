using System;
using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Warehouse;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemDetailsWhenChangedConsumer, DomainEvents.InventoryAdjusted>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemDetailsWhenChangedConsumer, DomainEvents.InventoryItemReceived>(registration);
        cfg.ConfigureEventReceiveEndpoint<ReserveInventoryItemWhenCartItemAddedConsumer, ECommerce.Contracts.ShoppingCart.DomainEvents.CartItemAdded>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"warehouse.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
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