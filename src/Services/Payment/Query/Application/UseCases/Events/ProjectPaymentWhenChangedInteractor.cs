using Application.Abstractions;
using Contracts.Services.Payment;

namespace Application.UseCases.Events;

public interface IProjectPaymentWhenChangedInteractor :
    IInteractor<DomainEvent.PaymentCanceled>,
    IInteractor<DomainEvent.PaymentRequested> { }

public class ProjectPaymentWhenChangedInteractor : IProjectPaymentWhenChangedInteractor
{
    private readonly IProjectionGateway<Projection.PaymentDetails> _projectionGateway;

    public ProjectPaymentWhenChangedInteractor(IProjectionGateway<Projection.PaymentDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task InteractAsync(DomainEvent.PaymentCanceled @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.PaymentId,
            field: payment => payment.Status,
            value: @event.Status,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentRequested @event, CancellationToken cancellationToken)
    {
        Projection.PaymentDetails paymentDetails = new(
            @event.PaymentId,
            @event.OrderId,
            @event.Amount,
            @event.Status,
            false);

        return _projectionGateway.UpsertAsync(paymentDetails, cancellationToken);
    }
}