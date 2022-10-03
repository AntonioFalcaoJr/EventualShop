using Application.EventStore;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Commands;

public class DecreaseCartItemConsumer : IConsumer<Command.DecreaseCartItem>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public DecreaseCartItemConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.DecreaseCartItem> context)
    {
        var shoppingCart = await _eventStore.LoadAsync(context.Message.Id, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStore.AppendAsync(shoppingCart, context.CancellationToken);
    }
}