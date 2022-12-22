using Application.Abstractions.Handlers;
using Application.Handlers.Emails;
using Application.Handlers.PushesMobile;
using Contracts.Services.Communication;
using Domain.Aggregates;
using Domain.ValueObject;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Abstractions.Gateways;

public abstract class NotificationGateway : INotificationGateway
{
    private readonly INotificationHandler _notificationHandler;

    protected NotificationGateway(
        IEmailNotificationHandler emailNotificationHandler,
        IPushMobileNotificationHandler pushMobileNotificationHandler)
    {
        emailNotificationHandler
            .SetNext(pushMobileNotificationHandler);

        _notificationHandler = emailNotificationHandler;
    }

    public async Task NotifyAsync(Notification notification, CancellationToken cancellationToken)
    {
        foreach (var method in notification.Methods)
        {
            var result = await _notificationHandler.HandleAsync((handler, option) => handler.NotifyAsync(option, cancellationToken), method.Option, cancellationToken);

            notification.Handle(result is { }
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

            notification.Handle(new Command.CancelNotificationMethod(notification.Id, method.Id));
        }
    }
}