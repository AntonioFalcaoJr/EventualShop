using Domain.Enumerations;
using Domain.ValueObject;

namespace Application.Abstractions.Handlers;

public interface INotificationHandler
{
    INotificationHandler? Next { get; }
    INotificationHandler SetNext(INotificationHandler next);
    Task<NotificationMethodStatus> HandleAsync(Func<INotificationHandler, INotificationOption, Task<NotificationMethodStatus>> operation, INotificationOption option, CancellationToken cancellationToken);
    Task<NotificationMethodStatus> NotifyAsync(INotificationOption option, CancellationToken cancellationToken);
    Task<NotificationMethodStatus> CancelAsync(INotificationOption option, CancellationToken cancellationToken);
}