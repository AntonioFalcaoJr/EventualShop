using Application.EventStore;
using Contracts.Services.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.Commands;

public class DiscardCartConsumer : IConsumer<Command.DiscardCart>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public DiscardCartConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.DiscardCart> context)
    {
        var shoppingCart = await _eventStore.LoadAggregateAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStore.AppendEventsAsync(shoppingCart, context.CancellationToken);
    }
}