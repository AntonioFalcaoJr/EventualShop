using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;
using Commands = ECommerce.Contracts.Orders.Commands;

namespace Application.UseCases.EventHandlers.Integrations;

public class PlaceOrderWhenCartSubmittedConsumer : IConsumer<IntegrationEvents.CartSubmitted>
{
    private readonly IOrderEventStoreService _eventStoreService;

    public PlaceOrderWhenCartSubmittedConsumer(IOrderEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<IntegrationEvents.CartSubmitted> context)
    {
        var order = new Order();

        order.Handle(new Commands.PlaceOrder(
            context.Message.Customer,
            context.Message.ShoppingCartItems,
            context.Message.Total,
            context.Message.PaymentMethods));

        await _eventStoreService.AppendEventsToStreamAsync(order, context.CancellationToken);
    }
}