using Domain.Enumerations;
using Domain.ValueObject;

namespace Application.Abstractions.Gateways;

public interface INotificationOptionGateway
{
    Task<NotificationMethodStatus> NotifyAsync(INotificationOption option, CancellationToken cancellationToken);
    Task<NotificationMethodStatus> CancelAsync(INotificationOption option, CancellationToken cancellationToken);
}

public interface INotificationOptionGateway<in TOption> : INotificationOptionGateway
    where TOption : INotificationOption
{
    Task<NotificationMethodStatus> NotifyAsync(TOption option, CancellationToken cancellationToken);

    Task<NotificationMethodStatus> CancelAsync(TOption option, CancellationToken cancellationToken);

    Task<NotificationMethodStatus> INotificationOptionGateway.NotifyAsync(INotificationOption option, CancellationToken cancellationToken)
        => NotifyAsync((TOption)option, cancellationToken);

    Task<NotificationMethodStatus> INotificationOptionGateway.CancelAsync(INotificationOption option, CancellationToken cancellationToken)
        => CancelAsync((TOption)option, cancellationToken);
}