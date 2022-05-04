using Application.EventStore;
using Contracts.Services.ShoppingCart;
using MassTransit;
using Command = Contracts.Services.Warehouse.Command;

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
        
        // TODO - review default values
        var inventoryItem = await _eventStore.LoadAggregateAsync(context.Message.Product.Id ?? default, context.CancellationToken);

        inventoryItem.Handle(
            new Command.ReserveInventory(
                context.Message.Product.Id ?? default,
                context.Message.CartId,
                context.Message.Product.Sku,
                context.Message.Quantity));

        await _eventStore.AppendEventsAsync(inventoryItem, context.CancellationToken);
    }
}