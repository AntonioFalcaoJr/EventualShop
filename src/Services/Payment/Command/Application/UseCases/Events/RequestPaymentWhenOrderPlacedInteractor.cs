using Application.Abstractions;
using Application.Services;
using Domain.Aggregates;
using Order = Contracts.Services.Order;
using Contracts.Services.Payment;

namespace Application.UseCases.Events;

public interface IRequestPaymentWhenOrderPlacedInteractor : IInteractor<Order.DomainEvent.OrderPlaced> { }

public class RequestPaymentWhenOrderPlacedInteractor : IRequestPaymentWhenOrderPlacedInteractor
{
    private readonly IApplicationService _applicationService;

    public RequestPaymentWhenOrderPlacedInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Order.DomainEvent.OrderPlaced @event, CancellationToken cancellationToken)
    {
        Payment payment = new();

        payment.Handle(new Command.RequestPayment(
            @event.OrderId,
            @event.Total,
            @event.BillingAddress,
            @event.PaymentMethods));

        await _applicationService.AppendEventsAsync(payment, cancellationToken);
    }
}