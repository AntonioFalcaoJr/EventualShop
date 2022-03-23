using Application.EventSourcing.EventStore;
using MassTransit;
using DecreaseShoppingCartItemCommand = ECommerce.Contracts.ShoppingCart.Commands.DecreaseShoppingCartItem;

namespace Application.UseCases.Commands;

public class DecreaseShoppingCartItemConsumer : IConsumer<DecreaseShoppingCartItemCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public DecreaseShoppingCartItemConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DecreaseShoppingCartItemCommand> context)
    {
        var shoppingCart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
    }
}