using Domain.ValueObject;

namespace Application.Abstractions.Gateways;

public interface IEmailGateway
{
    Task SendHtmlEmailAsync(Email email, CancellationToken cancellationToken);
    Task SendTextEmailAsync(Email email, CancellationToken cancellationToken);
}