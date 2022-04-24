using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;
using Command = ECommerce.Contracts.Orders.Command;

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
        var order = new Order();

        order.Handle(new Command.PlaceOrder(
            context.Message.Customer,
            context.Message.ShoppingCartItems,
            context.Message.Total,
            context.Message.PaymentMethods));

        await _eventStore.AppendEventsAsync(order, context.CancellationToken);
    }
}