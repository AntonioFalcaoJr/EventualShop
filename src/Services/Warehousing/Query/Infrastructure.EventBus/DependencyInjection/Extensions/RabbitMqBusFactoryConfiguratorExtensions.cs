using Contracts.Abstractions.Messages;
using Contracts.Boundaries.Warehouse;
using Infrastructure.EventBus.Consumers.Events;
using MassTransit;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryGridItemWhenInventoryChangedConsumer, DomainEvent.InventoryCreated>(context);

        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemListItemWhenInventoryItemChangedConsumer, DomainEvent.InventoryItemIncreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemListItemWhenInventoryItemChangedConsumer, DomainEvent.InventoryItemReceived>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemListItemWhenInventoryItemChangedConsumer, DomainEvent.InventoryAdjustmentIncreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemListItemWhenInventoryItemChangedConsumer, DomainEvent.InventoryAdjustmentDecreased>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"warehousing.query.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}