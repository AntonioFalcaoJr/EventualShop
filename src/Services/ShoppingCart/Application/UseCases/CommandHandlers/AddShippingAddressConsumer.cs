using Application.EventStore;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class AddShippingAddressConsumer : IConsumer<Commands.AddShippingAddress>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public AddShippingAddressConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.AddShippingAddress> context)
    {
        var shoppingCart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
    }
}