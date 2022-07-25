using Application.EventStore;
using Contracts.Services.Warehouse;
using MassTransit;
using Command = Contracts.Services.ShoppingCart.Command;

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
        var shoppingCart = await _eventStore.LoadAsync(context.Message.CartId, context.CancellationToken);

        shoppingCart.Handle(
            new Command.ConfirmCartItem(
                context.Message.CartId,
                context.Message.Sku,
                context.Message.Quantity,
                context.Message.Expiration));

        await _eventStore.AppendAsync(shoppingCart, context.CancellationToken);
    }
}