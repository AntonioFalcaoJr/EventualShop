using Application.EventStore;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class AddCreditCardConsumer : IConsumer<Commands.AddCreditCard>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public AddCreditCardConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.AddCreditCard> context)
    {
        var shoppingCart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
    }
}