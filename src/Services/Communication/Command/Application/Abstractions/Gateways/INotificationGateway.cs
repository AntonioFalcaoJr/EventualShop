using Domain.Enumerations;
using Domain.ValueObject;

namespace Application.Abstractions.Gateways;

public interface INotificationGateway<in TOption>
    where TOption : INotificationOption
{
    Task<NotificationMethodStatus> NotifyAsync(TOption option, CancellationToken cancellationToken);
    Task<NotificationMethodStatus> CancelAsync(TOption option, CancellationToken cancellationToken);
}