using Application.EventStore;
using Contracts.Services.ShoppingCarts;
using Domain.Aggregates;
using MassTransit;
using Command = Contracts.Services.Orders.Command;

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