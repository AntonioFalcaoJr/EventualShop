﻿using Application.Abstractions;
using Contracts.Services.Payment;

namespace Application.UseCases.Events;

public interface IProjectPaymentMethodDetailsWhenChangedInteractor :
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

    public Task InteractAsync(DomainEvent.PaymentMethodAuthorized @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, @event.Version, @event.Status, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodDenied @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, @event.Version, @event.Status, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodCanceled @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, @event.Version, @event.Status, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodCancellationDenied @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, @event.Version, @event.Status, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodRefunded @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, @event.Version, @event.Status, cancellationToken);

    public Task InteractAsync(DomainEvent.PaymentMethodRefundDenied @event, CancellationToken cancellationToken)
        => UpdateStatusAsync(@event.PaymentMethodId, @event.Version, @event.Status, cancellationToken);

    private Task UpdateStatusAsync(Guid methodId, long version, string status, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: methodId, 
            version: version, 
            field: method => method.Status, 
            value: status, 
            cancellationToken: cancellationToken);
}