using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using CartSubmittedEvent = ECommerce.Contracts.ShoppingCart.IntegrationEvents.CartSubmitted;
using PlaceOrderCommand = ECommerce.Contracts.Order.Commands.PlaceOrder;

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

        order.Handle(new PlaceOrderCommand(
            context.Message.CustomerId,
            context.Message.CartItems,
            context.Message.Total,
            context.Message.BillingAddress,
            context.Message.ShippingAddress,
            context.Message.PaymentMethods));

        await _eventStoreService.AppendEventsToStreamAsync(order, context.Message, context.CancellationToken);
    }
}