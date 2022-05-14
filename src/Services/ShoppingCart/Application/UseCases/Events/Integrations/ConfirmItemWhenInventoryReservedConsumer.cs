using Application.EventStore;
using Contracts.Services.Warehouse;
using MassTransit;

namespace Application.UseCases.Events.Integrations;

public class ConfirmItemWhenInventoryReservedConsumer : IConsumer<DomainEvent.InventoryReserved>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public ConfirmItemWhenInventoryReservedConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<DomainEvent.InventoryReserved> context)
    {
        var shoppingCart = await _eventStore.LoadAggregateAsync(context.Message.CartId, context.CancellationToken);

        shoppingCart.Handle();

        await context.Publish(cartSubmittedEvent, context.CancellationToken);
    }
}