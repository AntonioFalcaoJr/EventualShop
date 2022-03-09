using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using AddCreditCardCommand = ECommerce.Contracts.ShoppingCart.Commands.AddCreditCard;

namespace Application.UseCases.Commands;

public class AddCreditCardConsumer : IConsumer<AddCreditCardCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public AddCreditCardConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<AddCreditCardCommand> context)
    {
        var shoppingCart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
        shoppingCart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
    }
}