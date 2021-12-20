using Application.EventSourcing.EventStore;
using MassTransit;
using AddCartItemCommand = ECommerce.Contracts.ShoppingCart.Commands.AddCartItem;

namespace Application.UseCases.Commands;

public class AddCartItemConsumer : IConsumer<AddCartItemCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public AddCartItemConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<AddCartItemCommand> context)
    {
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        cart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(cart, context.CancellationToken);
    }
}