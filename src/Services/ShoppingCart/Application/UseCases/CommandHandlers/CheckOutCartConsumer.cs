using Application.EventStore;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class CheckOutCartConsumer : IConsumer<Commands.CheckOutCart>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public CheckOutCartConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.CheckOutCart> context)
    {
        var shoppingCart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
    }
}