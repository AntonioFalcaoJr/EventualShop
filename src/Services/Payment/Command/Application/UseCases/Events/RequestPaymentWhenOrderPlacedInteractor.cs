using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Order;
using Domain.Aggregates;
using Command = Contracts.Boundaries.Payment.Command;

namespace Application.UseCases.Events;

public interface IRequestPaymentWhenOrderPlacedInteractor : IInteractor<DomainEvent.OrderPlaced> { }

public class RequestPaymentWhenOrderPlacedInteractor(IApplicationService service) : IRequestPaymentWhenOrderPlacedInteractor
{
    public async Task InteractAsync(DomainEvent.OrderPlaced @event, CancellationToken cancellationToken)
    {
        Payment payment = new();

        payment.Handle(new Command.RequestPayment(
            @event.OrderId,
            @event.Total,
            @event.BillingAddress,
            @event.PaymentMethods));

        await service.AppendEventsAsync(payment, cancellationToken);
    }
}