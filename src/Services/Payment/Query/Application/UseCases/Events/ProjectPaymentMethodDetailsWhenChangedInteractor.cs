using Application.Abstractions;
using Contracts.Services.Payment;

namespace Application.UseCases.Events;

public interface IProjectPaymentMethodDetailsWhenChangedInteractor :
    IInteractor<DomainEvent.PaymentRequested>,
    IInteractor<DomainEvent.PaymentMethodAuthorized>,
    IInteractor<DomainEvent.PaymentMethodDenied>,
    IInteractor<DomainEvent.PaymentMethodCanceled>,
    IInteractor<DomainEvent.PaymentMethodCancellationDenied>,
    IInteractor<DomainEvent.PaymentMethodRefunded>,
    IInteractor<DomainEvent.PaymentMethodRefundDenied> { }

public class ProjectPaymentMethodDetailsWhenChangedInteractor : IProjectPaymentMethodDetailsWhenChangedInteractor
{
    private readonly IProjectionGateway<Projection.PaymentMethodDetails> _projectionGateway;

    public ProjectPaymentMethodDetailsWhenChangedInteractor(IProjectionGateway<Projection.PaymentMethodDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task InteractAsync(DomainEvent.PaymentRequested @event, CancellationToken cancellationToken)
    {
        
        // TODO: Fix nesting 
        // var methods = @event.PaymentMethods.Select(method
        //     => new Projection.PaymentMethodDetails(
        //         method.Id,
        //         @event.PaymentId,
        //         method.Amount,
        //         method.Option,
        //         method.Status,
        //         false));
        //
        // return _projectionGateway.UpsertManyAsync(methods, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.PaymentMethodAuthorized @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, @event.Status, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodDenied @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, @event.Status, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodCanceled @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, @event.Status, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodCancellationDenied @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, @event.Status, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodRefunded @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, @event.Status, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodRefundDenied @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, @event.Status, cancellationToken);

    private Task UpdateStatusAsync(Guid methodId, string status, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(methodId, method => method.Status, status, cancellationToken);
}