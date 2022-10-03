using Application.EventStore;
using Contracts.Services.Warehouse;
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
        var inventory = await _eventStore.LoadAsync(context.Message.Id, context.CancellationToken);
        inventory.Handle(context.Message);
        await _eventStore.AppendAsync(inventory, context.CancellationToken);
    }
}