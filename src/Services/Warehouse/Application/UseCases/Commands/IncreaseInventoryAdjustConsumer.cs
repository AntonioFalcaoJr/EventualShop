using Application.EventStore;
using Contracts.Services.Warehouse;
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
        var inventory = await _eventStore.LoadAggregateAsync(context.Message.InventoryId, context.CancellationToken);
        inventory.Handle(context.Message);
        await _eventStore.AppendEventsAsync(inventory, context.CancellationToken);
    }
}