using Application.Abstractions;
using Application.Abstractions.Gateways;
using Application.Services;
using Contracts.Services.Communication;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public interface ISendNotificationWhenNotificationRequestedInteractor : IInteractor<DomainEvent.NotificationRequested> { }

public class SendNotificationWhenNotificationRequestedInteractor : ISendNotificationWhenNotificationRequestedInteractor
{
    private readonly IApplicationService _applicationService;
    private readonly INotificationService _notificationService;

    public SendNotificationWhenNotificationRequestedInteractor(IApplicationService applicationService, INotificationService notificationService)
    {
        _applicationService = applicationService;
        _notificationService = notificationService;
    }

    public async Task InteractAsync(DomainEvent.NotificationRequested @event, CancellationToken cancellationToken)
    {
        var notification = await _applicationService.LoadAggregateAsync<Notification>(@event.NotificationId, cancellationToken);
        await _notificationService.NotifyAsync(notification, cancellationToken);
        await _applicationService.AppendEventsAsync(notification, cancellationToken);
    }
}