using Application.Abstractions;
using Application.Abstractions.Gateways;
using Application.Services;
using Contracts.Boundaries.Notification;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public interface ISendNotificationWhenNotificationRequestedInteractor : IInteractor<DomainEvent.NotificationRequested> { }

public class SendNotificationWhenNotificationRequestedInteractor(IApplicationService service, INotificationService notificationService)
    : ISendNotificationWhenNotificationRequestedInteractor
{
    public async Task InteractAsync(DomainEvent.NotificationRequested @event, CancellationToken cancellationToken)
    {
        var notification = await service.LoadAggregateAsync<Notification>(@event.NotificationId, cancellationToken);
        await notificationService.NotifyAsync(notification, cancellationToken);
        await service.AppendEventsAsync(notification, cancellationToken);
    }
}