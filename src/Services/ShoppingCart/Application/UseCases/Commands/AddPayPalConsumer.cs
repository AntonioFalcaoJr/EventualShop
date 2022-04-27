using Application.EventStore;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Commands;

public class AddPayPalConsumer : IConsumer<Command.AddPayPal>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public AddPayPalConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.AddPayPal> context)
    {
        var shoppingCart = await _eventStore.LoadAggregateAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStore.AppendEventsAsync(shoppingCart, context.CancellationToken);
    }
}