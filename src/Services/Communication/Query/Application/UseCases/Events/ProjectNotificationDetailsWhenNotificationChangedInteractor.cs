using Application.Abstractions;
using Contracts.Services.Communication;

namespace Application.UseCases.Events;

public interface IProjectNotificationDetailsWhenNotificationChangedInteractor : IInteractor<DomainEvent.NotificationRequested> { }

public class ProjectNotificationDetailsWhenNotificationChangedInteractor : IProjectNotificationDetailsWhenNotificationChangedInteractor
{
    private readonly IProjectionGateway<Projection.NotificationDetails> _projectionGateway;

    public ProjectNotificationDetailsWhenNotificationChangedInteractor(IProjectionGateway<Projection.NotificationDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.NotificationRequested @event, CancellationToken cancellationToken)
    {
        Projection.NotificationDetails notificationDetails = new(@event.NotificationId, @event.Methods, false);
        await _projectionGateway.InsertAsync(notificationDetails, cancellationToken);
    }
}