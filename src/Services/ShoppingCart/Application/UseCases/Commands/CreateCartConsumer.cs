using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.Commands;

public class CreateCartConsumer : IConsumer<Command.CreateCart>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public CreateCartConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.CreateCart> context)
    {
        var shoppingCart = new ShoppingCart();
        shoppingCart.Handle(context.Message);
        await _eventStore.AppendEventsAsync(shoppingCart, context.CancellationToken);
    }
}