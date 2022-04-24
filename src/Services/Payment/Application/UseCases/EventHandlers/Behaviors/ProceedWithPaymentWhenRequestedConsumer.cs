using Application.EventStore;
using Application.Services;
using ECommerce.Contracts.Payments;
using MassTransit;

namespace Application.UseCases.EventHandlers.Behaviors;

public class ProceedWithPaymentWhenRequestedConsumer : IConsumer<DomainEvent.PaymentRequested>
{
    private readonly IPaymentEventStoreService _eventStoreService;
    private readonly IPaymentStrategy _paymentStrategy;

    public ProceedWithPaymentWhenRequestedConsumer(
        IPaymentEventStoreService eventStoreService,
        IPaymentStrategy paymentStrategy)
    {
        _eventStoreService = eventStoreService;
        _paymentStrategy = paymentStrategy;
    }

    public async Task Consume(ConsumeContext<DomainEvent.PaymentRequested> context)
    {
        var payment = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.PaymentId, context.CancellationToken);
        await _paymentStrategy.AuthorizePaymentAsync(payment, context.CancellationToken);
        payment.Handle(new Command.ProceedWithPayment(payment.Id, payment.OrderId));
        await _eventStoreService.AppendEventsToStreamAsync(payment, context.CancellationToken);
    }
}