using Domain.ValueObject;

namespace Application.Abstractions.Gateways;

public interface IPushMobileGateway
{
    Task SendPushAsync(PushMobile push, CancellationToken cancellationToken);
}