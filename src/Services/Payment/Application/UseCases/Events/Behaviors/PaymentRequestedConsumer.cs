using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.Services;
using Domain.Aggregates;
using MassTransit;
using PaymentRequestedEvent = Messages.Services.Payments.DomainEvents.PaymentRequested;

namespace Application.UseCases.Events.Behaviors;

public class PaymentRequestedConsumer : IConsumer<PaymentRequestedEvent>
{
    private readonly IPaymentEventStoreService _eventStoreService;
    private readonly IPaymentStrategy _strategy;

    public PaymentRequestedConsumer(IPaymentEventStoreService eventStoreService, IPaymentStrategy strategy)
    {
        _eventStoreService = eventStoreService;
        _strategy = strategy;
    }

    public async Task Consume(ConsumeContext<PaymentRequestedEvent> context)
    {
        var payment = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.PaymentId, context.CancellationToken);
        await _strategy.ProceedWithPaymentAsync(payment, context.CancellationToken);
        payment.Handle(new Messages.Services.Payments.Commands.ProceedWithPayment(context.Message.PaymentId, context.Message.OrderId));
        await _eventStoreService.AppendEventsToStreamAsync(payment, context.CancellationToken);
    }
}