using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using Contracts.Abstractions;
using Contracts.Services.Warehouse;
using MassTransit;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryWhenChangedConsumer, DomainEvent.InventoryCreated>(registration);
        
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemWhenChangedConsumer, DomainEvent.InventoryReceived>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemWhenChangedConsumer, DomainEvent.InventoryAdjustmentDecreased>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemWhenChangedConsumer, DomainEvent.InventoryAdjustmentIncreased>(registration);
        
        cfg.ConfigureEventReceiveEndpoint<ReserveInventoryItemWhenCartItemAddedConsumer, Contracts.Services.ShoppingCart.DomainEvent.CartItemAdded>(registration);
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