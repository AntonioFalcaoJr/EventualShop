using Application.EventStore;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Commands;

public class ChangeCartItemQuantityConsumer : IConsumer<Command.ChangeCartItemQuantity>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public ChangeCartItemQuantityConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.ChangeCartItemQuantity> context)
    {
        var shoppingCart = await _eventStore.LoadAsync(context.Message.Id, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStore.AppendAsync(shoppingCart, context.CancellationToken);
    }
}