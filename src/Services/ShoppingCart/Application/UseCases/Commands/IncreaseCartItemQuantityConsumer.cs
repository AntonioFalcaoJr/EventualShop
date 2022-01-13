using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using IncreaseCartItemQuantityCommand =  ECommerce.Contracts.ShoppingCart.Commands.IncreaseCartItemQuantity;

namespace Application.UseCases.Commands;

public class IncreaseCartItemQuantityConsumer : IConsumer<IncreaseCartItemQuantityCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public IncreaseCartItemQuantityConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<IncreaseCartItemQuantityCommand> context)
    {
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        cart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(cart, context.Message, context.CancellationToken);
    }
}