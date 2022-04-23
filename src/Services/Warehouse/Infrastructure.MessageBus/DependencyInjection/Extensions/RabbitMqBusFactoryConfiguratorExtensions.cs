using Application.UseCases.EventHandlers.Integrations;
using Application.UseCases.EventHandlers.Projections;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemWhenChangedConsumer, DomainEvents.InventoryAdjusted>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemWhenChangedConsumer, DomainEvents.InventoryReceived>(registration);
        cfg.ConfigureEventReceiveEndpoint<ReserveInventoryItemWhenCartItemAddedConsumer, ECommerce.Contracts.ShoppingCarts.DomainEvents.CartItemAdded>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext registration)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"warehouse.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(registration);
            });
}