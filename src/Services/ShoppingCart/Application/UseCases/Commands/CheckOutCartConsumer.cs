using Application.EventStore;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Commands;

public class CheckOutCartConsumer : IConsumer<Command.CheckOutCart>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public CheckOutCartConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.CheckOutCart> context)
    {
        var shoppingCart = await _eventStore.LoadAsync(context.Message.Id, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStore.AppendAsync(shoppingCart, context.CancellationToken);
    }
}