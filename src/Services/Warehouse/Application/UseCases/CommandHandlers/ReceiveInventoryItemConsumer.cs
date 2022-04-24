using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class ReceiveInventoryItemConsumer : IConsumer<Command.ReceiveInventoryItem>
{
    private readonly IWarehouseEventStoreService _eventStoreService;

    public ReceiveInventoryItemConsumer(IWarehouseEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.ReceiveInventoryItem> context)
    {
        var inventoryItem = new InventoryItem();
        inventoryItem.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(inventoryItem, context.CancellationToken);
    }
}