using Application.EventStore;
using Application.Services;
using Contracts.Services.Payment;
using MassTransit;

namespace Application.UseCases.Events.Behaviors;

public class ProceedWithPaymentWhenRequestedConsumer : IConsumer<DomainEvent.PaymentRequested>
{
    private readonly IPaymentEventStoreService _eventStore;
    private readonly IPaymentGateway _paymentGateway;

    public ProceedWithPaymentWhenRequestedConsumer(
        IPaymentEventStoreService eventStore,
        IPaymentGateway paymentGateway)
    {
        _eventStore = eventStore;
        _paymentGateway = paymentGateway;
    }

    public async Task Consume(ConsumeContext<DomainEvent.PaymentRequested> context)
    {
        var payment = await _eventStore.LoadAggregateAsync(context.Message.PaymentId, context.CancellationToken);
        await _paymentGateway.AuthorizeAsync(payment, context.CancellationToken);
        payment.Handle(new Command.ProceedWithPayment(payment.Id, payment.OrderId));
        await _eventStore.AppendEventsAsync(payment, context.CancellationToken);
    }
}