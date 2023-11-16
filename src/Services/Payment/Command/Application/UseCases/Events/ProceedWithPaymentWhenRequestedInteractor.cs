using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Payment;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public interface IProceedWithPaymentWhenRequestedInteractor : IInteractor<DomainEvent.PaymentRequested> { }

public class ProceedWithPaymentWhenRequestedInteractor(IApplicationService service,
        IPaymentGateway paymentGateway)
    : IProceedWithPaymentWhenRequestedInteractor
{
    public async Task InteractAsync(DomainEvent.PaymentRequested @event, CancellationToken cancellationToken)
    {
        var payment = await service.LoadAggregateAsync<Payment>(@event.PaymentId, cancellationToken);
        await paymentGateway.AuthorizeAsync(payment, cancellationToken);
        payment.Handle(new Command.ProceedWithPayment(payment.Id, payment.OrderId));
        await service.AppendEventsAsync(payment, cancellationToken);
    }
}