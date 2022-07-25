using Application.EventStore;
using Contracts.Services.ShoppingCart;
using Domain.Aggregates;
using MassTransit;
using Command = Contracts.Services.Order.Command;

namespace Application.UseCases.Events.Integrations;

public class PlaceOrderWhenCartSubmittedConsumer : IConsumer<IntegrationEvent.CartSubmitted>
{
    private readonly IOrderEventStoreService _eventStore;

    public PlaceOrderWhenCartSubmittedConsumer(IOrderEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<IntegrationEvent.CartSubmitted> context)
    {
        Order order = new();

        order.Handle(new Command.PlaceOrder(
            context.Message.CartId,
            context.Message.CustomerId,
            context.Message.Total,
            context.Message.BillingAddress,
            context.Message.ShippingAddress,
            context.Message.Items,
            context.Message.PaymentMethods));

        await _eventStore.AppendAsync(order, context.CancellationToken);
    }
}