using Domain.ValueObject;

namespace Application.Abstractions.Handlers;

public abstract class NotificationHandler<TOption> : INotificationHandler<TOption>
    where TOption : INotificationOption
{
    protected INotificationHandler<INotificationOption> Next;

    public INotificationHandler<INotificationOption> SetNext<T>(INotificationHandler<T> next)
        where T : INotificationOption
        => Next = next as INotificationHandler<INotificationOption>;

    public Task<INotificationResult> HandleAsync(
        Func<INotificationHandler<INotificationOption>, INotificationOption, Task<INotificationResult>> onHandle,
        INotificationOption option,
        CancellationToken cancellationToken)
        => option is TOption ? onHandle((INotificationHandler<INotificationOption>)this, option) : Next.HandleAsync(onHandle, option, cancellationToken);

    public abstract Task<INotificationResult> NotifyAsync(TOption option, CancellationToken cancellationToken);
    public abstract Task<INotificationResult> CancelAsync(TOption option, CancellationToken cancellationToken);
}