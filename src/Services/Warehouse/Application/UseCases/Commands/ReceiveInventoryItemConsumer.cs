using Application.EventStore;
using Contracts.Services.Warehouse;
using Domain.Aggregates;
using MassTransit;

namespace Application.UseCases.Commands;

public class ReceiveInventoryItemConsumer : IConsumer<Command.ReceiveInventoryItem>
{
    private readonly IWarehouseEventStoreService _eventStore;

    public ReceiveInventoryItemConsumer(IWarehouseEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.ReceiveInventoryItem> context)
    {
        InventoryItem inventoryItem = new();
        inventoryItem.Handle(context.Message);
        await _eventStore.AppendEventsAsync(inventoryItem, context.CancellationToken);
    }
}