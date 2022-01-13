using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using AddAddPayPalCommand = ECommerce.Contracts.ShoppingCart.Commands.AddPayPal;

namespace Application.UseCases.Commands;

public class AddPayPalConsumer : IConsumer<AddAddPayPalCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public AddPayPalConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<AddAddPayPalCommand> context)
    {
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        cart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(cart, context.Message, context.CancellationToken);
    }
}