﻿using System.Net;
using System.Net.Mail;
using Application.Abstractions.Gateways;
using Domain.ValueObject;
using Infrastructure.SMTP.DependencyInjection.Options;
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
            EnableSsl = _options.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_options.Username, _options.Password)
        };
    }

    public Task SendHtmlEmailAsync(Email email, CancellationToken cancellationToken)
        => _smtpClient.SendMailAsync(new(_options.Username, email.Address, email.Subject, email.Body) { IsBodyHtml = true }, cancellationToken);

    public Task SendTextEmailAsync(Email email, CancellationToken cancellationToken)
        => _smtpClient.SendMailAsync(new(_options.Username, email.Address, email.Subject, email.Body), cancellationToken);
}