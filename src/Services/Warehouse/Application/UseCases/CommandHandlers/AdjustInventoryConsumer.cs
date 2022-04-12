using Application.EventSourcing.EventStore;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class AdjustInventoryConsumer : IConsumer<Commands.AdjustInventory>
{
    private readonly IWarehouseEventStoreService _eventStoreService;

    public AdjustInventoryConsumer(IWarehouseEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.AdjustInventory> context)
    {
        var inventoryItem = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.ProductId, context.CancellationToken);
        inventoryItem.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(inventoryItem, context.CancellationToken);
    }
}