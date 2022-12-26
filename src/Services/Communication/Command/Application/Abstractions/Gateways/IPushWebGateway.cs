using Domain.ValueObject;

namespace Application.Abstractions.Gateways;

public interface IPushWebGateway
{
    Task SendPushAsync(PushWeb push, CancellationToken cancellationToken);
}