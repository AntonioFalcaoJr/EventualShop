using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.Commands;

public class CreateCartConsumer : IConsumer<Command.CreateCart>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public CreateCartConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.CreateCart> context)
    {
        var shoppingCart = new ShoppingCart();
        shoppingCart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
    }
}