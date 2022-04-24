using Application.EventStore;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;
using Command = ECommerce.Contracts.Warehouses.Command;

namespace Application.UseCases.Events.Integrations;

public class ReserveInventoryItemWhenCartItemAddedConsumer : IConsumer<DomainEvent.CartItemAdded>
{
    private readonly IWarehouseEventStoreService _eventStore;

    public ReserveInventoryItemWhenCartItemAddedConsumer(IWarehouseEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<DomainEvent.CartItemAdded> context)
    {
        var inventoryItem = await _eventStore.LoadAggregateAsync(context.Message.Item.ProductId, context.CancellationToken);

        inventoryItem.Handle(
            new Command.ReserveInventory(
                context.Message.Item.ProductId,
                context.Message.CartId,
                context.Message.Item.Sku,
                context.Message.Item.Quantity));

        await _eventStore.AppendEventsAsync(inventoryItem, context.CancellationToken);
    }
}