using Domain.Entities;

namespace Application.Abstractions.Services;

public interface INotificationService
{
    INotificationService SetNext(INotificationService next);
    Task<INotificationResult> HandleAsync(Func<INotificationService, NotificationMethod, CancellationToken, Task<INotificationResult>> behaviorProcessor, NotificationMethod method, CancellationToken cancellationToken);
    Task<INotificationResult> NotifyAsync(NotificationMethod method, CancellationToken cancellationToken);
    Task<INotificationResult> CancelAsync(NotificationMethod method, CancellationToken cancellationToken);
}