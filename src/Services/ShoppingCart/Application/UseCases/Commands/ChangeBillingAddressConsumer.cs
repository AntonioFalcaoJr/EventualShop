using Application.EventStore;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Commands;

public class ChangeBillingAddressConsumer : IConsumer<Command.ChangeBillingAddress>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public ChangeBillingAddressConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.ChangeBillingAddress> context)
    {
        var shoppingCart = await _eventStore.LoadAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStore.AppendAsync(shoppingCart, context.CancellationToken);
    }
}