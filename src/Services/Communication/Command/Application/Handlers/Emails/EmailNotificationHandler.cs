using Application.Abstractions.Gateways;
using Application.Abstractions.Handlers;
using Domain.Enumerations;
using Domain.ValueObject;

namespace Application.Handlers.Emails;

public class EmailNotificationHandler : IEmailNotificationHandler
{
    private readonly IEmailGateway _emailGateway;
    public INotificationHandler? Next { get; private set; }

    public EmailNotificationHandler(IEmailGateway emailGateway)
    {
        _emailGateway = emailGateway;
    }

    public INotificationHandler SetNext(INotificationHandler next)
        => Next = next;

    public Task<NotificationMethodStatus> HandleAsync(Func<INotificationHandler, INotificationOption, Task<NotificationMethodStatus>> operation, INotificationOption option, CancellationToken cancellationToken)
        => option is Email email ? operation(this, email) : Next?.HandleAsync(operation, option, cancellationToken) ?? Task.FromResult(NotificationMethodStatus.Failed);

    public Task<NotificationMethodStatus> NotifyAsync(INotificationOption option, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<NotificationMethodStatus> CancelAsync(INotificationOption option, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}