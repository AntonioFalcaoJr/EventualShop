using Application.EventSourcing.EventStore;
using MassTransit;
using AdjustInventoryCommand = ECommerce.Contracts.Warehouse.Commands.AdjustInventory;

namespace Application.UseCases.Commands;

public class AdjustInventoryConsumer : IConsumer<AdjustInventoryCommand>
{
    private readonly IWarehouseEventStoreService _eventStoreService;

    public AdjustInventoryConsumer(IWarehouseEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<AdjustInventoryCommand> context)
    {
        var product = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.ProductId, context.CancellationToken);
        product.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(product, context.CancellationToken);
    }
}