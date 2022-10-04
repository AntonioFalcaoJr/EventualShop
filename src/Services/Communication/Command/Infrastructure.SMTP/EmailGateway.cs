using System.Net;
using System.Net.Mail;
using Application.Abstractions.Gateways;
using Application.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.SMTP;

public class EmailGateway : IEmailGateway
{
    private readonly SmtpOptions _options;
    private readonly SmtpClient _smtpClient;

    public EmailGateway(IOptionsSnapshot<SmtpOptions> options)
    {
        _options = options.Value;

        _smtpClient = new()
        {
            Host = _options.Host,
            Port = _options.Port,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_options.Username, _options.Password)
        };
    }

    public Task SendHtmlEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)
        => _smtpClient.SendMailAsync(new(_options.Username, to, subject, body) { IsBodyHtml = true }, cancellationToken);

    public Task SendTextEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)
        => _smtpClient.SendMailAsync(new(_options.Username, to, subject, body), cancellationToken);
}