using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Application.Services;
using Contracts.Services.Communication;
using Domain.Aggregates;

namespace Application.UseCases;

public class NotificationRequestedInteractor : IInteractor<DomainEvent.NotificationRequested>
{
    private readonly IApplicationService _applicationService;
    private readonly INotificationService _notificationService;

    public NotificationRequestedInteractor(IApplicationService applicationService, INotificationService notificationService)
    {
        _applicationService = applicationService;
        _notificationService = notificationService;
    }
    
    public async Task InteractAsync(DomainEvent.NotificationRequested @event, CancellationToken cancellationToken)
    {
        var notification = await _applicationService.LoadAggregateAsync<Notification>(@event.NotificationId, cancellationToken);
        await _notificationService.NotifyAsync(notification as Notification, cancellationToken);
        await _applicationService.AppendEventsAsync(notification, cancellationToken);
    }
}