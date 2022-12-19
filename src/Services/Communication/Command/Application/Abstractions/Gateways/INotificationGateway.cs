using Domain.Aggregates;

namespace Application.Abstractions.Gateways;

public interface INotificationGateway
{
    Task NotifyAsync(Notification notification, CancellationToken cancellationToken);
    Task CancelAsync(Notification notification, CancellationToken cancellationToken);
}