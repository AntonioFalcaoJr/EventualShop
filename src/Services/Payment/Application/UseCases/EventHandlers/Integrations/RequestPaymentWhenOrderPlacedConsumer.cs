using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Payments;
using MassTransit;
using DomainEvents = ECommerce.Contracts.Orders.DomainEvents;

namespace Application.UseCases.EventHandlers.Integrations;

public class RequestPaymentWhenOrderPlacedConsumer : IConsumer<DomainEvents.OrderPlaced>
{
    private readonly IPaymentEventStoreService _eventStoreService;

    public RequestPaymentWhenOrderPlacedConsumer(IPaymentEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DomainEvents.OrderPlaced> context)
    {
        var payment = new Payment();

        payment.Handle(new Command.RequestPayment(
            context.Message.OrderId,
            context.Message.Total,
            context.Message.Customer.BillingAddress,
            context.Message.PaymentMethods));

        await _eventStoreService.AppendEventsToStreamAsync(payment, context.CancellationToken);
    }
}