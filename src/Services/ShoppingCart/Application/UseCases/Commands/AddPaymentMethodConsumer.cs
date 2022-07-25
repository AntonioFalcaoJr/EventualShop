using Application.EventStore;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Commands;

public class AddPaymentMethodConsumer : IConsumer<Command.AddPaymentMethod>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public AddPaymentMethodConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.AddPaymentMethod> context)
    {
        var shoppingCart = await _eventStore.LoadAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStore.AppendAsync(shoppingCart, context.CancellationToken);
    }
}