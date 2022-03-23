using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using CreateCartCommand = ECommerce.Contracts.ShoppingCart.Commands.CreateCart;

namespace Application.UseCases.Commands;

public class CreateCartConsumer : IConsumer<CreateCartCommand>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public CreateCartConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<CreateCartCommand> context)
    {
        var shoppingCart = new ShoppingCart();
        shoppingCart.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
    }
}