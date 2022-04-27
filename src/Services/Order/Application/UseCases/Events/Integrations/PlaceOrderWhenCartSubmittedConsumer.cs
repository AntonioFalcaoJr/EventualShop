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
        var order = new Order();

        order.Handle(new Command.PlaceOrder(
            context.Message.Customer,
            context.Message.ShoppingCartItems,
            context.Message.Total,
            context.Message.PaymentMethods));

        await _eventStore.AppendEventsAsync(order, context.CancellationToken);
    }
}