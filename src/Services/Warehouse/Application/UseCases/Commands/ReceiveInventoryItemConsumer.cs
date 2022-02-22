using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using ReceiveInventoryItemCommand = ECommerce.Contracts.Warehouse.Commands.ReceiveInventoryItem;

namespace Application.UseCases.Commands;

public class ReceiveInventoryItemConsumer : IConsumer<ReceiveInventoryItemCommand>
{
    private readonly IWarehouseEventStoreService _eventStoreService;

    public ReceiveInventoryItemConsumer(IWarehouseEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<ReceiveInventoryItemCommand> context)
    {
        var inventoryItem = new InventoryItem();
        inventoryItem.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(inventoryItem, context.CancellationToken);
    }
}