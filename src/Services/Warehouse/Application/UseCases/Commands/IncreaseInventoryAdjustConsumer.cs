using Application.EventStore;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.Commands;

public class IncreaseInventoryAdjustConsumer : IConsumer<Command.IncreaseInventoryAdjust>
{
    private readonly IWarehouseEventStoreService _eventStore;

    public IncreaseInventoryAdjustConsumer(IWarehouseEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.IncreaseInventoryAdjust> context)
    {
        var inventoryItem = await _eventStore.LoadAggregateAsync(context.Message.ProductId, context.CancellationToken);
        inventoryItem.Handle(context.Message);
        await _eventStore.AppendEventsAsync(inventoryItem, context.CancellationToken);
    }
}