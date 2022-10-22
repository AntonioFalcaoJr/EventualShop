using Application.EventStore;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Commands;

public class AddBillingAddressConsumer : IConsumer<Command.AddBillingAddress>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public AddBillingAddressConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.AddBillingAddress> context)
    {
        var shoppingCart = await _eventStore.LoadAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStore.AppendAsync(shoppingCart, context.CancellationToken);
    }
}