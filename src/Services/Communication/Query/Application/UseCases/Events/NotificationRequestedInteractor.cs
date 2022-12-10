using Application.Abstractions;
using Contracts.Services.Communication;

namespace Application.UseCases.Events;

public class NotificationRequestedInteractor : IInteractor<DomainEvent.NotificationRequested>
{
    private readonly IProjectionGateway<Projection.NotificationDetails> _projectionGateway;

    public NotificationRequestedInteractor(IProjectionGateway<Projection.NotificationDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.NotificationRequested @event, CancellationToken cancellationToken)
    {
        Projection.NotificationDetails notificationDetails = new(@event.NotificationId, @event.Methods, false);
        await _projectionGateway.InsertAsync(notificationDetails, cancellationToken);
    }
}