using Application.EventSourcing.EventStore;
using MassTransit;
using CheckOutCartCommand = ECommerce.Contracts.ShoppingCart.Commands.CheckOutCart;

namespace Application.UseCases.Commands;

public class CheckOutCartConsumer : IConsumer<CheckOutCartCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public CheckOutCartConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<CheckOutCartCommand> context)
    {
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        cart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(cart, context.CancellationToken);
    }
}