using Application.EventStore;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;
using Commands = ECommerce.Contracts.Warehouses.Commands;

namespace Application.UseCases.EventHandlers.Integrations;

public class ReserveInventoryItemWhenCartItemAddedConsumer : IConsumer<DomainEvents.CartItemAdded>
{
    private readonly IWarehouseEventStoreService _eventStoreService;

    public ReserveInventoryItemWhenCartItemAddedConsumer(IWarehouseEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DomainEvents.CartItemAdded> context)
    {
        var inventoryItem = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Item.ProductId, context.CancellationToken);

        inventoryItem.Handle(
            new Commands.ReserveInventory(
                context.Message.Item.ProductId,
                context.Message.CartId,
                context.Message.Item.Sku,
                context.Message.Item.Quantity));

        await _eventStoreService.AppendEventsToStreamAsync(inventoryItem, context.CancellationToken);
    }
}