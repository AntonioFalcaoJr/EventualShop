using Application.EventStore;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.Commands;

public class DecreaseInventoryAdjustConsumer : IConsumer<Command.DecreaseInventoryAdjust>
{
    private readonly IWarehouseEventStoreService _eventStore;

    public DecreaseInventoryAdjustConsumer(IWarehouseEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.DecreaseInventoryAdjust> context)
    {
        var inventoryItem = await _eventStore.LoadAggregateAsync(context.Message.ProductId, context.CancellationToken);
        inventoryItem.Handle(context.Message);
        await _eventStore.AppendEventsAsync(inventoryItem, context.CancellationToken);
    }
}