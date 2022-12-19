using Application.Abstractions.Gateways;
using Application.Abstractions.Handlers;
using Domain.ValueObject;

namespace Application.Handlers.Emails;

public class EmailHandler : NotificationHandler<Email>
{
    private readonly IEmailGateway _emailGateway;

    public EmailHandler(IEmailGateway emailGateway)
    {
        _emailGateway = emailGateway;
    }

    public override async Task<INotificationResult> NotifyAsync(Email email, CancellationToken cancellationToken)
    {
         await _emailGateway.SendHtmlEmailAsync(email, cancellationToken);
         return new EmailNotificationResult { Success = true };
    }

    public override Task<INotificationResult> CancelAsync(Email option, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}