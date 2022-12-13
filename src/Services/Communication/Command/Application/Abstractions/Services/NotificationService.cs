using Domain.Entities;

namespace Application.Abstractions.Services;

public abstract class NotificationService : INotificationService
{
    private INotificationService? _next;

    public INotificationService SetNext(INotificationService next)
        => _next = next;

    public virtual Task<INotificationResult> HandleAsync(Func<INotificationService, NotificationMethod, CancellationToken, Task<INotificationResult?>> behaviorProcessor, NotificationMethod method,
        CancellationToken cancellationToken)
        => _next.HandleAsync(behaviorProcessor, method, cancellationToken);

    public abstract Task<INotificationResult?> NotifyAsync(NotificationMethod method, CancellationToken cancellationToken);
    public abstract Task<INotificationResult> CancelAsync(NotificationMethod method, CancellationToken cancellationToken);
    public abstract Task<INotificationResult> RefundAsync(NotificationMethod method, CancellationToken cancellationToken);
}