using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using AddShippingAddressCommand = ECommerce.Contracts.ShoppingCart.Commands.AddShippingAddress;

namespace Application.UseCases.Commands;

public class AddShippingAddressConsumer : IConsumer<AddShippingAddressCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public AddShippingAddressConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<AddShippingAddressCommand> context)
    {
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        cart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(cart, context.Message, context.CancellationToken);
    }
}