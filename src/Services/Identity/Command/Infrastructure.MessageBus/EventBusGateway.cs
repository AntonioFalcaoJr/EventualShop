using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using MassTransit;

namespace Infrastructure.MessageBus;

public class EventBusGateway : IEventBusGateway
{
    private readonly IBus _bus;

    public EventBusGateway(IBus bus)
        => _bus = bus;

    public Task PublishAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken)
        => Task.WhenAll(events.Select(@event => _bus.Publish(@event, @event.GetType(), cancellationToken)));

    public Task SchedulePublishAsync(DateTimeOffset scheduledTime, IEvent @event, CancellationToken cancellationToken)
        => _bus.CreateMessageScheduler().SchedulePublish(scheduledTime.UtcDateTime, @event, @event.GetType(), cancellationToken);
}