using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using MassTransit;

namespace Infrastructure.MessageBus;

public class EventBusGateway : IEventBusGateway
{
    private readonly IBus _bus;

    public EventBusGateway(IBus bus)
    {
        _bus = bus;
    }

    public Task PublishAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken)
        => Task.WhenAll(events.Select(@event => _bus.Publish(@event, @event.GetType(), cancellationToken)));

    public Task PublishAsync(IEvent @event, CancellationToken cancellationToken)
        => _bus.Publish(@event, @event.GetType(), cancellationToken);

    public Task SchedulePublishAsync(IDelayedEvent @event, DateTimeOffset scheduledTime, CancellationToken cancellationToken)
        => _bus.CreateMessageScheduler(_bus.Topology).SchedulePublish(scheduledTime.UtcDateTime, @event, @event.GetType(), cancellationToken);
}