using System.Net.Mail;
using Application.Abstractions.Gateways;
using Domain.Enumerations;
using Domain.ValueObject;
using Infrastructure.SMTP.DependencyInjection.Options;
using Microsoft.Extensions.Options;
using Serilog;

namespace Infrastructure.SMTP;

public class EmailGateway : IEmailGateway
{
    private readonly SmtpOptions _options;
    private readonly SmtpClient _smtpClient;

    public EmailGateway(IOptionsSnapshot<SmtpOptions> options, SmtpClient smtpClient)
    {
        _smtpClient = smtpClient;
        _options = options.Value;
    }

    public Task<NotificationMethodStatus> NotifyAsync(Email email, CancellationToken cancellationToken)
        => SendAsync(client => client.SendMailAsync(new(_options.Username, email.Address, email.Subject, email.Body) { IsBodyHtml = true }, cancellationToken));

    public Task<NotificationMethodStatus> CancelAsync(Email option, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    private async Task<NotificationMethodStatus> SendAsync(Func<SmtpClient, Task> sendEmailAsync)
    {
        var onSendCompleted = () => NotificationMethodStatus.Pending;

        _smtpClient.SendCompleted += (_, @event) =>
        {
            onSendCompleted = @event switch
            {
                { Error: { } error } => () =>
                {
                    Log.Error(error, "Error sending email");
                    return NotificationMethodStatus.Failed;
                },
                { Cancelled: true } => () =>
                {
                    Log.Warning("Email sending cancelled");
                    return NotificationMethodStatus.Cancelled;
                },
                _ => () =>
                {
                    Log.Information("Email sent successfully");
                    return NotificationMethodStatus.Sent;
                }
            };
        };

        await sendEmailAsync(_smtpClient);

        return onSendCompleted();
    }
}