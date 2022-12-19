using Application.Abstractions.Handlers;
using Contracts.Services.Communication;
using Domain.Aggregates;
using Domain.ValueObject;

namespace Application.Abstractions.Gateways;

public abstract class NotificationGateway : INotificationGateway
{
    private readonly INotificationHandler<INotificationOption> _notificationHandler;

    protected NotificationGateway(
        INotificationHandler<Email> emailHandler,
        INotificationHandler<Sms> smsHandler,
        INotificationHandler<PushWeb> pushWebHandler,
        INotificationHandler<PushMobile> pushMobileHandler)
    {
        emailHandler
            .SetNext(smsHandler)
            .SetNext(pushWebHandler)
            .SetNext(pushMobileHandler);

        _notificationHandler = emailHandler 
            as INotificationHandler<INotificationOption>;
    }

    public async Task NotifyAsync(Notification notification, CancellationToken cancellationToken)
    {
        foreach (var method in notification.Methods)
        {
            var result = await _notificationHandler.HandleAsync((handler, option)
                => handler.NotifyAsync(option, cancellationToken), method.Option, cancellationToken);

            notification.Handle(result.Success
                ? new Command.EmitNotificationMethod(notification.Id, method.Id)
                : new Command.FailNotificationMethod(notification.Id, method.Id));
        }
    }

    public async Task CancelAsync(Notification notification, CancellationToken cancellationToken)
    {
        foreach (var method in notification.Methods)
        {
            var result = await _notificationHandler.HandleAsync((handler, option)
                => handler.CancelAsync(option, cancellationToken), method.Option, cancellationToken);

            if (result.Success is false) return;

            notification.Handle(new Command.CancelNotificationMethod(notification.Id, method.Id));
        }
    }
}