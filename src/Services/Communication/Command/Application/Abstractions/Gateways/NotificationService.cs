using Contracts.Services.Communication;
using Domain.Aggregates;
using Domain.Enumerations;
using Domain.ValueObject;

namespace Application.Abstractions.Gateways;

public class NotificationService : INotificationService
{
    private readonly ILazy<INotificationGateway<Email>> _emailGateway;
    private readonly ILazy<INotificationGateway<Sms>> _smsGateway;
    private readonly ILazy<INotificationGateway<PushMobile>> _pushMobileGateway;
    private readonly ILazy<INotificationGateway<PushWeb>> _pushWebGateway;

    public NotificationService(
        ILazy<INotificationGateway<Email>> emailGateway,
        ILazy<INotificationGateway<Sms>> smsGateway,
        ILazy<INotificationGateway<PushMobile>> pushMobileGateway,
        ILazy<INotificationGateway<PushWeb>> pushWebGateway)
    {
        _emailGateway = emailGateway;
        _smsGateway = smsGateway;
        _pushMobileGateway = pushMobileGateway;
        _pushWebGateway = pushWebGateway;
    }

    public async Task NotifyAsync(Notification notification, CancellationToken cancellationToken)
    {
        foreach (var method in notification.Methods)
        {
            var status = await InvokeGateway(method.Option, (gateway, option) => gateway.NotifyAsync(option, cancellationToken));

            notification.Handle(status switch
            {
                NotificationMethodStatus.SentStatus => new Command.SendNotificationMethod(notification.Id, method.Id),
                NotificationMethodStatus.CancelledStatus => new Command.CancelNotificationMethod(notification.Id, method.Id),
                _ => new Command.FailNotificationMethod(notification.Id, method.Id),
            });
        }
    }

    public async Task CancelAsync(Notification notification, CancellationToken cancellationToken)
    {
        foreach (var method in notification.Methods)
        {
            var status = await InvokeGateway(method.Option, (gateway, option) => gateway.CancelAsync(option, cancellationToken));

            notification.Handle(status switch
            {
                // TODO
            });
        }
    }

    private Task<NotificationMethodStatus> InvokeGateway<T>(T option, Func<INotificationGateway<T>, INotificationOption, Task<NotificationMethodStatus>> operation)
        where T : class, INotificationOption
        => option switch
        {
            Email email => operation((INotificationGateway<T>)_emailGateway.Instance, email),
            Sms sms => operation((INotificationGateway<T>)_smsGateway.Instance, sms),
            PushMobile pushMobile => operation((INotificationGateway<T>)_pushMobileGateway.Instance, pushMobile),
            PushWeb pushWeb => operation((INotificationGateway<T>)_pushWebGateway.Instance, pushWeb),
            _ => new(() => NotificationMethodStatus.Failed)
        };
}