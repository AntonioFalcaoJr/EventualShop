using Application.EventStore;
using Contracts.Services.Warehouse;
using Domain.Aggregates;
using MassTransit;

namespace Application.UseCases.Commands;

public class CreateInventoryConsumer : IConsumer<Command.CreateInventory>
{
    private readonly IWarehouseEventStoreService _eventStore;

    public CreateInventoryConsumer(IWarehouseEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.CreateInventory> context)
    {
        Inventory inventory = new();
        inventory.Handle(context.Message);
        await _eventStore.AppendEventsAsync(inventory, context.CancellationToken);
    }
}