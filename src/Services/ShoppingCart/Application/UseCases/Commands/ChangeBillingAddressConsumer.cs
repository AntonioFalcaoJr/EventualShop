using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using ChangeBillingAddressCommand = ECommerce.Contracts.ShoppingCart.Commands.ChangeBillingAddress;

namespace Application.UseCases.Commands;

public class ChangeBillingAddressConsumer : IConsumer<ChangeBillingAddressCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public ChangeBillingAddressConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<ChangeBillingAddressCommand> context)
    {
        var shoppingCart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
    }
}