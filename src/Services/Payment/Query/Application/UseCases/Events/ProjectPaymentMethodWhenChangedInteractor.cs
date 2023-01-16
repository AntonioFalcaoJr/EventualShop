using Application.Abstractions;
using Contracts.Services.Payment;
using Domain.Enumerations;

namespace Application.UseCases.Events;

public interface IProjectPaymentMethodWhenChangedInteractor :
    IInteractor<DomainEvent.PaymentRequested>,
    IInteractor<DomainEvent.PaymentMethodAuthorized>,
    IInteractor<DomainEvent.PaymentMethodDenied>,
    IInteractor<DomainEvent.PaymentMethodCanceled>,
    IInteractor<DomainEvent.PaymentMethodCancellationDenied>,
    IInteractor<DomainEvent.PaymentMethodRefunded>,
    IInteractor<DomainEvent.PaymentMethodRefundDenied> { }

public class ProjectPaymentMethodWhenChangedInteractor : IProjectPaymentMethodWhenChangedInteractor
{
    private readonly IProjectionGateway<Projection.PaymentMethod> _projectionGateway;

    public ProjectPaymentMethodWhenChangedInteractor(IProjectionGateway<Projection.PaymentMethod> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task InteractAsync(DomainEvent.PaymentRequested @event, CancellationToken cancellationToken)
    {
        var methods = @event.PaymentMethods.Select(method
            => new Projection.PaymentMethod(
                method.Id,
                @event.PaymentId,
                method.Amount.value,
                method.Option,
                PaymentMethodStatus.Ready,
                false));

        return _projectionGateway.UpsertManyAsync(methods, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.PaymentMethodAuthorized @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, PaymentMethodStatus.Authorized, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodDenied @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, PaymentMethodStatus.Denied, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodCanceled @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, PaymentMethodStatus.Canceled, cancellationToken);
    
     
    public Task InteractAsync(DomainEvent.PaymentMethodCancellationDenied @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, PaymentMethodStatus.CancellationDenied, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodRefunded @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, PaymentMethodStatus.Refunded, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodRefundDenied @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, PaymentMethodStatus.RefundDenied, cancellationToken);

    private Task UpdateStatusAsync(Guid methodId, PaymentMethodStatus status, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(methodId, method => method.Status, status.Name, cancellationToken);
}