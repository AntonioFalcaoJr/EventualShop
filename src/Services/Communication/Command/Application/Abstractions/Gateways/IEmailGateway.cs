namespace Application.Abstractions.Gateways;

public interface IEmailGateway
{
    Task SendHtmlEmailAsync(string to, string subject, string body, CancellationToken cancellationToken);
    Task SendTextEmailAsync(string to, string subject, string body, CancellationToken cancellationToken);
}