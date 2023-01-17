using Application.Abstractions;
using Application.Services;
using Contracts.Services.Payment;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public interface IProceedWithPaymentWhenRequestedInteractor : IInteractor<DomainEvent.PaymentRequested> { }

public class ProceedWithPaymentWhenRequestedInteractor : IProceedWithPaymentWhenRequestedInteractor
{
    private readonly IApplicationService _applicationService;
    private readonly IPaymentGateway _paymentGateway;

    public ProceedWithPaymentWhenRequestedInteractor(
        IApplicationService applicationService,
        IPaymentGateway paymentGateway)
    {
        _applicationService = applicationService;
        _paymentGateway = paymentGateway;
    }

    public async Task InteractAsync(DomainEvent.PaymentRequested @event, CancellationToken cancellationToken)
    {
        var payment = await _applicationService.LoadAggregateAsync<Payment>(@event.PaymentId, cancellationToken);
        await _paymentGateway.AuthorizeAsync(payment, cancellationToken);
        payment.Handle(new Command.ProceedWithPayment(payment.Id, payment.OrderId));
        await _applicationService.AppendEventsAsync(payment, cancellationToken);
    }
}