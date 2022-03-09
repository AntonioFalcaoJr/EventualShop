using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using IncreaseShoppingCartItemCommand =  ECommerce.Contracts.ShoppingCart.Commands.IncreaseShoppingCartItem;

namespace Application.UseCases.Commands;

public class IncreaseShoppingCartItemConsumer : IConsumer<IncreaseShoppingCartItemCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public IncreaseShoppingCartItemConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<IncreaseShoppingCartItemCommand> context)
    {
        var shoppingCart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
    }
}