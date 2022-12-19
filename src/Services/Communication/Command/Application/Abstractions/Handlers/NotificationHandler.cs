using Domain.ValueObject;

namespace Application.Abstractions.Handlers;

public abstract class NotificationHandler<TOption> : INotificationHandler<TOption>
    where TOption : INotificationOption
{
    private INotificationHandler<INotificationOption> Next;

    public INotificationHandler<INotificationOption> SetNext<T>(INotificationHandler<T> next)
        where T : INotificationOption
        => Next = (INotificationHandler<INotificationOption>)next;

    public Task<INotificationResult> HandleAsync(
        Func<INotificationHandler<INotificationOption>, INotificationOption, CancellationToken, Task<INotificationResult>> onHandle,
        INotificationOption option,
        CancellationToken cancellationToken)
        => option is TOption
            ? onHandle((INotificationHandler<INotificationOption>)this, option, cancellationToken)
            : Next.HandleAsync(onHandle, option, cancellationToken);

    public abstract Task<INotificationResult> NotifyAsync(TOption option, CancellationToken cancellationToken);
    public abstract Task<INotificationResult> CancelAsync(TOption option, CancellationToken cancellationToken);
}