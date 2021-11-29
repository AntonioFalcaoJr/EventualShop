using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using CartSubmittedEvent = Messages.Services.ShoppingCarts.IntegrationEvents.CartSubmitted;
using PlaceOrderCommand = Messages.Services.Orders.Commands.PlaceOrder;

namespace Application.UseCases.Events.Integrations;

public class PlaceOrderWhenCartSubmittedConsumer : IConsumer<CartSubmittedEvent>
{
    private readonly IOrderEventStoreService _eventStoreService;

    public PlaceOrderWhenCartSubmittedConsumer(IOrderEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<CartSubmittedEvent> context)
    {
        var order = new Order();

        var placeOrder = new PlaceOrderCommand(
            context.Message.CustomerId,
            context.Message.CartItems,
            context.Message.BillingAddress,
            context.Message.CreditCard,
            context.Message.ShippingAddress);

        order.Handle(placeOrder);

        await _eventStoreService.AppendEventsToStreamAsync(order, context.CancellationToken);
    }
}