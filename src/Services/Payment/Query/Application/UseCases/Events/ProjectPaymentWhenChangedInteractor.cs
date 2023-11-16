using Application.Abstractions;
using Contracts.Boundaries.Payment;

namespace Application.UseCases.Events;

public interface IProjectPaymentWhenChangedInteractor :
    IInteractor<DomainEvent.PaymentCanceled>,
    IInteractor<DomainEvent.PaymentRequested> { }

public class ProjectPaymentWhenChangedInteractor(IProjectionGateway<Projection.PaymentDetails> projectionGateway)
    : IProjectPaymentWhenChangedInteractor
{
    public Task InteractAsync(DomainEvent.PaymentCanceled @event, CancellationToken cancellationToken)
        => projectionGateway.UpdateFieldAsync(
            id: @event.PaymentId,
            version: @event.Version,
            field: payment => payment.Status,
            value: @event.Status,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.PaymentRequested @event, CancellationToken cancellationToken)
    {
        Projection.PaymentDetails paymentDetails = new(
            @event.PaymentId,
            @event.OrderId,
            @event.Amount,
            @event.Status,
            false,
            @event.Version);

        await projectionGateway.ReplaceInsertAsync(paymentDetails, cancellationToken);
    }
}