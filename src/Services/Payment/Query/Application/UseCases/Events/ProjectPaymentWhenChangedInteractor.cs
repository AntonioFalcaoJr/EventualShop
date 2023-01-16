using Application.Abstractions;
using Contracts.Services.Payment;
using Domain.Enumerations;

namespace Application.UseCases.Events;

public interface IProjectPaymentWhenChangedInteractor :
    IInteractor<DomainEvent.PaymentCanceled>,
    IInteractor<DomainEvent.PaymentRequested> { }

public class ProjectPaymentWhenChangedInteractor : IProjectPaymentWhenChangedInteractor
{
    private readonly IProjectionGateway<Projection.Payment> _projectionGateway;

    public ProjectPaymentWhenChangedInteractor(IProjectionGateway<Projection.Payment> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }


    public Task InteractAsync(DomainEvent.PaymentCanceled @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.PaymentId,
            field: payment => payment.Status,
            value: PaymentStatus.Canceled.Name,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentRequested @event, CancellationToken cancellationToken)
    {
        Projection.Payment payment = new(
            @event.PaymentId,
            @event.OrderId,
            @event.Amount.value,
            @event.BillingAddress,
            @event.PaymentMethods,
            @event.Status,
            false);

        return _projectionGateway.UpsertAsync(payment, cancellationToken);
    }
}