using Application.EventStore;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;
using Command = ECommerce.Contracts.Warehouses.Command;

namespace Application.UseCases.EventHandlers.Integrations;

public class ReserveInventoryItemWhenCartItemAddedConsumer : IConsumer<DomainEvent.CartItemAdded>
{
    private readonly IWarehouseEventStoreService _eventStoreService;

    public ReserveInventoryItemWhenCartItemAddedConsumer(IWarehouseEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DomainEvent.CartItemAdded> context)
    {
        var inventoryItem = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Item.ProductId, context.CancellationToken);

        inventoryItem.Handle(
            new Command.ReserveInventory(
                context.Message.Item.ProductId,
                context.Message.CartId,
                context.Message.Item.Sku,
                context.Message.Item.Quantity));

        await _eventStoreService.AppendEventsToStreamAsync(inventoryItem, context.CancellationToken);
    }
}