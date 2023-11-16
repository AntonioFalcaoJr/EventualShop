using Application.Abstractions;
using Contracts.Boundaries.Notification;

namespace Application.UseCases.Events;

public interface IProjectNotificationDetailsWhenNotificationChangedInteractor : IInteractor<DomainEvent.NotificationRequested> { }

public class ProjectNotificationDetailsWhenNotificationChangedInteractor(IProjectionGateway<Projection.NotificationDetails> projectionGateway)
    : IProjectNotificationDetailsWhenNotificationChangedInteractor
{
    public async Task InteractAsync(DomainEvent.NotificationRequested @event, CancellationToken cancellationToken)
    {
        Projection.NotificationDetails notificationDetails = new(
            @event.NotificationId,
            false,
            @event.Version);

        await projectionGateway.ReplaceInsertAsync(notificationDetails, cancellationToken);
    }
}