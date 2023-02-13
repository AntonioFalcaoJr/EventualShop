using Contracts.Abstractions.Messages;

namespace Application.Abstractions.Gateways;

public interface IEventBusGateway
{
    Task PublishAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken);
    Task PublishAsync(IEvent @event, CancellationToken cancellationToken);
    Task SchedulePublishAsync(IDelayedEvent @event, DateTimeOffset scheduledTime, CancellationToken cancellationToken);
}