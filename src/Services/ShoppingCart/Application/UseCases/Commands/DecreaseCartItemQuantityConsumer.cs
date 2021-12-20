using Application.EventSourcing.EventStore;
using MassTransit;
using DecreaseCartItemQuantityCommand = ECommerce.Contracts.ShoppingCart.Commands.DecreaseCartItemQuantity;

namespace Application.UseCases.Commands;

public class DecreaseCartItemQuantityConsumer : IConsumer<DecreaseCartItemQuantityCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public DecreaseCartItemQuantityConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DecreaseCartItemQuantityCommand> context)
    {
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        cart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(cart, context.CancellationToken);
    }
}