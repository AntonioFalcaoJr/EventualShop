using Application.Abstractions.Services;
using Application.Services.Emails;
using Application.Services.PushesMobile;
using Application.Services.PushesWeb;
using Application.Services.SMSs;
using Contracts.Services.Communication;
using Domain.Aggregates;

namespace Application.Services;

public class NotificationGateway : INotificationGateway
{
    private readonly INotificationService _notificationService;

    public NotificationGateway(
        IEmailService emailService,
        ISmsService smsService,
        IPushWebService pushWebService,
        IPushMobileService pushMobileService)
    {
        emailService
            .SetNext(smsService)
            .SetNext(pushWebService)
            .SetNext(pushMobileService);

        _notificationService = emailService;
    }

    public async Task NotifyAsync(Notification notification, CancellationToken cancellationToken)
    {
        foreach (var method in notification.Methods)
        {
            var result = await _notificationService.HandleAsync((srv, mtd, ct) => srv.NotifyAsync(mtd, ct), method, cancellationToken);

            notification.Handle(result.Success
                ? new Command.EmitNotificationMethod(notification.Id, method.Id)
                : new Command.FailNotificationMethod(notification.Id, method.Id));
        }
    }

    public async Task CancelAsync(Notification notification, CancellationToken cancellationToken)
    {
        foreach (var method in notification.Methods)
        {
            var result = await _notificationService.HandleAsync((srv, mtd, ct) => srv.CancelAsync(mtd, ct), method, cancellationToken);

            if (result.Success is false) return;
            
            notification.Handle(new Command.CancelNotificationMethod(notification.Id, method.Id));
        }
    }
}