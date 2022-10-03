using Application.EventStore;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Commands;

public class RemoveCartItemConsumer : IConsumer<Command.RemoveCartItem>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public RemoveCartItemConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.RemoveCartItem> context)
    {
        var shoppingCart = await _eventStore.LoadAsync(context.Message.Id, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStore.AppendAsync(shoppingCart, context.CancellationToken);
    }
}