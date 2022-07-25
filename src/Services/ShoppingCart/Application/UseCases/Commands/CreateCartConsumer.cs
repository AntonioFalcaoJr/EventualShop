using Application.EventStore;
using Contracts.Services.ShoppingCart;
using Domain.Aggregates;
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
        ShoppingCart shoppingCart = new();
        shoppingCart.Handle(context.Message);
        await _eventStore.AppendAsync(shoppingCart, context.CancellationToken);
    }
}