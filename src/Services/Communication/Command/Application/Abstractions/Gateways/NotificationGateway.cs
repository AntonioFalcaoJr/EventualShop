using Application.DependencyInjection;
using Contracts.Services.Communication;
using Domain.Aggregates;
using Domain.Enumerations;

namespace Application.Abstractions.Gateways;

public class NotificationGateway : INotificationGateway
{
    private readonly NotificationOptionGatewayProvider _gatewayProvider;

    public NotificationGateway(NotificationOptionGatewayProvider gatewayProvider)
    {
        _gatewayProvider = gatewayProvider;
    }

    public async Task NotifyAsync(Notification notification, CancellationToken cancellationToken)
    {
        foreach (var method in notification.Methods)
        {
            var status = await _gatewayProvider.GetGateway(method.Option).NotifyAsync(method.Option, cancellationToken);

            notification.Handle(status switch
            {
                NotificationMethodStatus.FailedStatus => new Command.FailNotificationMethod(notification.Id, method.Id),
                NotificationMethodStatus.SentStatus => new Command.EmitNotificationMethod(notification.Id, method.Id),
                { } when status == NotificationMethodStatus.Sent => new Command.EmitNotificationMethod(notification.Id, method.Id),
                _ or null => new Command.FailNotificationMethod(notification.Id, method.Id),
            });
        }
    }

    public async Task CancelAsync(Notification notification, CancellationToken cancellationToken)
    {
        // foreach (var method in notification.Methods)
        // {
        //     var status = await _notificationHandler.HandleAsync((handler, option, ct)
        //         => handler.CancelAsync(option, ct), method.Option, cancellationToken);
        //
        //     notification.Handle(status switch
        //     {
        //         { } when status == NotificationMethodStatus.FailedStatus => new Command.CancelNotificationMethod(notification.Id, method.Id),
        //         { } when status == NotificationMethodStatus.CancelledStatus => new Command.CancelNotificationMethod(notification.Id, method.Id),
        //         { } when status == NotificationMethodStatus.SentStatus => new Command.CancelNotificationMethod(notification.Id, method.Id),
        //         _ or null => new Command.CancelNotificationMethod(notification.Id, method.Id),
        //     });
        // }
    }
}