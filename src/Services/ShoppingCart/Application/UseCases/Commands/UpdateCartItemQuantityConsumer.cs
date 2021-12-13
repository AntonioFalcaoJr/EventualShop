using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using UpdateCartItemQuantityCommand = ECommerce.Contracts.ShoppingCart.Commands.UpdateCartItemQuantity;

namespace Application.UseCases.Commands;

public class UpdateCartItemQuantityConsumer : IConsumer<UpdateCartItemQuantityCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public UpdateCartItemQuantityConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<UpdateCartItemQuantityCommand> context)
    {
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        cart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(cart, context.CancellationToken);
    }
}