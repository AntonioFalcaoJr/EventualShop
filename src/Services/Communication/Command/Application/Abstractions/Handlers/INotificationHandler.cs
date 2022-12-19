using Domain.ValueObject;

namespace Application.Abstractions.Handlers;

public interface INotificationHandler<in TOption>
    where TOption : INotificationOption
{
    INotificationHandler<INotificationOption> SetNext<T>(INotificationHandler<T> next)
        where T : INotificationOption;

    Task<INotificationResult> HandleAsync(Func<INotificationHandler<INotificationOption>, INotificationOption, Task<INotificationResult>> onHandle, INotificationOption option, CancellationToken cancellationToken);
    Task<INotificationResult> NotifyAsync(TOption option, CancellationToken cancellationToken);
    Task<INotificationResult> CancelAsync(TOption option, CancellationToken cancellationToken);
}