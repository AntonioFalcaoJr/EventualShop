using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using Contracts.Abstractions.Messages;
using Contracts.Services.Warehouse;
using MassTransit;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryWhenChangedConsumer, DomainEvent.InventoryCreated>(context);
        
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemWhenChangedConsumer, DomainEvent.InventoryItemReceived>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemWhenChangedConsumer, DomainEvent.InventoryAdjustmentDecreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemWhenChangedConsumer, DomainEvent.InventoryAdjustmentIncreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectInventoryItemWhenChangedConsumer, DomainEvent.InventoryItemIncreased>(context);
        
        cfg.ConfigureEventReceiveEndpoint<ReserveInventoryItemWhenCartItemAddedConsumer, Contracts.Services.ShoppingCart.DomainEvent.CartItemAdded>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"warehouse.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}