using Domain.ValueObject;

namespace Application.Abstractions.Gateways;

public interface ISmsGateway
{
    Task SendSmsAsync(Sms sms, CancellationToken cancellationToken);
}