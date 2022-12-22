using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Application.Services;
using Contracts.Services.Communication;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public class NotificationRequestedInteractor : IInteractor<DomainEvent.NotificationRequested>
{
    private readonly IApplicationService _applicationService;
    private readonly INotificationGateway _notificationGateway;

    public NotificationRequestedInteractor(IApplicationService applicationService, INotificationGateway notificationGateway)
    {
        _applicationService = applicationService;
        _notificationGateway = notificationGateway;
    }
    
    public async Task InteractAsync(DomainEvent.NotificationRequested @event, CancellationToken cancellationToken)
    {
        var notification = await _applicationService.LoadAggregateAsync<Notification>(@event.NotificationId, cancellationToken);
        await _notificationGateway.NotifyAsync(notification as Notification, cancellationToken);
        await _applicationService.AppendEventsAsync(notification, cancellationToken);
    }
}