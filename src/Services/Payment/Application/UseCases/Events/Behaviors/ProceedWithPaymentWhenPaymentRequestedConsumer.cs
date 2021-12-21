using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.Services;
using MassTransit;
using PaymentRequestedEvent = ECommerce.Contracts.Payment.DomainEvents.PaymentRequested;
using ProceedWithPaymentCommand =ECommerce.Contracts.Payment.Commands.ProceedWithPayment;

namespace Application.UseCases.Events.Behaviors;

public class ProceedWithPaymentWhenPaymentRequestedConsumer : IConsumer<PaymentRequestedEvent>
{
    private readonly IPaymentEventStoreService _eventStoreService;
    private readonly IPaymentStrategy _paymentStrategy;

    public ProceedWithPaymentWhenPaymentRequestedConsumer(
        IPaymentEventStoreService eventStoreService, 
        IPaymentStrategy paymentStrategy)
    {
        _eventStoreService = eventStoreService;
        _paymentStrategy = paymentStrategy;
    }

    public async Task Consume(ConsumeContext<PaymentRequestedEvent> context)
    {
        var payment = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.PaymentId, context.CancellationToken);
        await _paymentStrategy.AuthorizePaymentAsync(payment, context.CancellationToken);
        payment.Handle(new ProceedWithPaymentCommand(payment.Id, payment.OrderId));
        await _eventStoreService.AppendEventsToStreamAsync(payment, context.CancellationToken);
    }
}