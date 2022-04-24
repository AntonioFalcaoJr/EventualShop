using Application.EventStore;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.Commands;

public class DiscardCartConsumer : IConsumer<Command.DiscardCart>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public DiscardCartConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.DiscardCart> context)
    {
        var shoppingCart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
    }
}