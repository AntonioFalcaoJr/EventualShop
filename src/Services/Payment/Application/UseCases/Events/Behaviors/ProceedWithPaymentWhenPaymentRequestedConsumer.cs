using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.Services;
using MassTransit;
using PaymentRequestedEvent = Messages.Services.Payments.DomainEvents.PaymentRequested;
using ProceedWithPaymentCommand = Messages.Services.Payments.Commands.ProceedWithPayment;

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
        await _paymentStrategy.ProceedWithPaymentAsync(payment, context.CancellationToken);
        payment.Handle(new ProceedWithPaymentCommand(payment.Id, payment.OrderId));
        await _eventStoreService.AppendEventsToStreamAsync(payment, context.CancellationToken);
    }
}