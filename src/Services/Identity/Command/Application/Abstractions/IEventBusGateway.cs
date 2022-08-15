using Contracts.Abstractions.Messages;

namespace Application.Abstractions;

public interface IEventBusGateway
{
    Task PublishAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken);
}