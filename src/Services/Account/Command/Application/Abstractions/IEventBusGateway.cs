using Contracts.Abstractions.Messages;

namespace Application.Abstractions;

public interface IEventBusGateway
{
    Task SendAsync(IEvent @event, string exchange, CancellationToken cancellationToken);
    Task PublishAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken);
    Task PublishAsync(IEvent @event, CancellationToken cancellationToken);
    Task SchedulePublishAsync(DateTimeOffset scheduledTime, IEvent @event, CancellationToken cancellationToken);
}