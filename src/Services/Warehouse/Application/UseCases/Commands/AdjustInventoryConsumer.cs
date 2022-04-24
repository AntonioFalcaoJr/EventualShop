using Application.EventStore;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.Commands;

public class AdjustInventoryConsumer : IConsumer<Command.AdjustInventory>
{
    private readonly IWarehouseEventStoreService _eventStore;

    public AdjustInventoryConsumer(IWarehouseEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.AdjustInventory> context)
    {
        var inventoryItem = await _eventStore.LoadAggregateAsync(context.Message.ProductId, context.CancellationToken);
        inventoryItem.Handle(context.Message);
        await _eventStore.AppendEventsAsync(inventoryItem, context.CancellationToken);
    }
}