using Application.EventSourcing.EventStore;
using MassTransit;
using CartItemAddedEvent = ECommerce.Contracts.ShoppingCart.DomainEvents.CartItemAdded;
using ReserveInventoryCommand = ECommerce.Contracts.Warehouse.Commands.ReserveInventory;

namespace Application.UseCases.Events.Integrations;

public class ReserveInventoryItemWhenCartItemAddedConsumer : IConsumer<CartItemAddedEvent>
{
    private readonly IWarehouseEventStoreService _eventStoreService;

    public ReserveInventoryItemWhenCartItemAddedConsumer(IWarehouseEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<CartItemAddedEvent> context)
    {
        var inventoryItem = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.ProductId, context.CancellationToken);

        inventoryItem.Handle(
            new ReserveInventoryCommand(
                context.Message.ProductId,
                context.Message.CartId,
                inventoryItem.Sku,
                context.Message.Quantity));

        await _eventStoreService.AppendEventsToStreamAsync(inventoryItem, context.CancellationToken);
    }
}