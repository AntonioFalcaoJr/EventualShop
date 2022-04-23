using Application.EventStore;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;
using Commands = ECommerce.Contracts.Warehouses.Commands;

namespace Application.UseCases.EventHandlers.Integrations;

public class ReserveInventoryItemWhenCartItemAddedConsumer : IConsumer<DomainEvents.CartItemAdded>
{
    private readonly IWarehouseEventStoreService _eventStoreService;

    public ReserveInventoryItemWhenCartItemAddedConsumer(IWarehouseEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DomainEvents.CartItemAdded> context)
    {
        var inventoryItem = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.ProductId, context.CancellationToken);

        inventoryItem.Handle(
            new Commands.ReserveInventory(
                context.Message.ProductId,
                context.Message.CartId,
                context.Message.Sku,
                context.Message.Quantity));

        await _eventStoreService.AppendEventsToStreamAsync(inventoryItem, context.CancellationToken);
    }
}