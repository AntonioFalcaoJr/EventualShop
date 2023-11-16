using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using MassTransit;

namespace Infrastructure.EventBus;

public class EventBusGateway(IBus bus, IPublishEndpoint publishEndpoint) : IEventBusGateway
{
    public Task PublishAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken)
        => Task.WhenAll(events.Select(@event => publishEndpoint.Publish(@event, @event.GetType(), cancellationToken)));

    public Task PublishAsync(IEvent @event, CancellationToken cancellationToken)
        => publishEndpoint.Publish(@event, @event.GetType(), cancellationToken);

    public Task SchedulePublishAsync(IDelayedEvent @event, DateTimeOffset scheduledTime, CancellationToken cancellationToken)
        => publishEndpoint.CreateMessageScheduler(bus.Topology).SchedulePublish(scheduledTime.UtcDateTime, @event, @event.GetType(), cancellationToken);
}