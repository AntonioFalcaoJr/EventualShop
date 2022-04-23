using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class CreateCartConsumer : IConsumer<Commands.CreateCart>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public CreateCartConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.CreateCart> context)
    {
        var shoppingCart = new ShoppingCart();
        shoppingCart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
    }
}