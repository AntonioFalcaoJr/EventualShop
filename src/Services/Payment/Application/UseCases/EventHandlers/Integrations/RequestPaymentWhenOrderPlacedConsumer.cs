using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Orders;
using MassTransit;
using Commands = ECommerce.Contracts.Payments.Commands;

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

        payment.Handle(new Commands.RequestPayment(
            context.Message.OrderId,
            context.Message.Total,
            context.Message.Customer.BillingAddress,
            context.Message.PaymentMethods));

        await _eventStoreService.AppendEventsToStreamAsync(payment, context.CancellationToken);
    }
}