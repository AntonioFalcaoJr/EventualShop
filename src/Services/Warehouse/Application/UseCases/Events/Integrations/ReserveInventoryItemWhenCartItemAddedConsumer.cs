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
        var inventory = await _eventStore.LoadAggregateAsync(context.Message.InventoryId, context.CancellationToken);

        inventory.Handle(
            new Command.ReserveInventoryItem(
                context.Message.CatalogId,
                context.Message.CartId,
                context.Message.Product,
                context.Message.Quantity));

        await _eventStore.AppendEventsAsync(inventory, context.CancellationToken);
    }
}