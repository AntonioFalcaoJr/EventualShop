using Application.EventStore;
using Contracts.Services.ShoppingCarts;
using MassTransit;
using Command = Contracts.Services.Warehouses.Command;

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
        var inventoryItem = await _eventStore.LoadAggregateAsync(context.Message.Product.Id, context.CancellationToken);

        inventoryItem.Handle(
            new Command.ReserveInventory(
                context.Message.Product.Id,
                context.Message.CartId,
                context.Message.Product.Sku,
                context.Message.Quantity));

        await _eventStore.AppendEventsAsync(inventoryItem, context.CancellationToken);
    }
}