using Contracts.Boundaries.Notification;
using Domain.Aggregates;
using Domain.Enumerations;
using Domain.ValueObject;

namespace Application.Abstractions.Gateways;

public class NotificationService(ILazy<INotificationGateway<Email>> emailGateway,
        ILazy<INotificationGateway<Sms>> smsGateway,
        ILazy<INotificationGateway<PushMobile>> pushMobileGateway,
        ILazy<INotificationGateway<PushWeb>> pushWebGateway)
    : INotificationService
{
    public async Task NotifyAsync(Notification notification, CancellationToken cancellationToken)
    {
        foreach (var method in notification.Methods)
        {
            var status = await InvokeGateway(method.Option, (gateway, option) => gateway.NotifyAsync(option, cancellationToken));

            notification.Handle(status switch
            {
                NotificationMethodStatus.SentStatus => new Command.SendNotificationMethod(notification.Id, method.Id),
                NotificationMethodStatus.CancelledStatus => new Command.CancelNotificationMethod(notification.Id, method.Id),
                _ => new Command.FailNotificationMethod(notification.Id, method.Id)
            });
        }
    }

    public async Task CancelAsync(Notification notification, CancellationToken cancellationToken)
    {
        foreach (var method in notification.Methods)
        {
            var status = await InvokeGateway(method.Option, (gateway, option) => gateway.CancelAsync(option, cancellationToken));

            // notification.Handle(status switch
            // {
            //     // TODO
            // });
        }
    }

    private Task<NotificationMethodStatus> InvokeGateway<T>(T option, Func<INotificationGateway<T>, INotificationOption, Task<NotificationMethodStatus>> operation)
        where T : class, INotificationOption
        => option switch
        {
            Email email => operation((INotificationGateway<T>)emailGateway.Instance, email),
            Sms sms => operation((INotificationGateway<T>)smsGateway.Instance, sms),
            PushMobile pushMobile => operation((INotificationGateway<T>)pushMobileGateway.Instance, pushMobile),
            PushWeb pushWeb => operation((INotificationGateway<T>)pushWebGateway.Instance, pushWeb),
            _ => new(() => NotificationMethodStatus.Failed)
        };
}