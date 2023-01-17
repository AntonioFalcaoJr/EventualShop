using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using MassTransit;

namespace Infrastructure.MessageBus;

public class EventBusGateway : IEventBusGateway
{
    private readonly IBus _bus;
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBusGateway(IBus bus, IPublishEndpoint publishEndpoint)
    {
        _bus = bus;
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken)
        => Task.WhenAll(events.Select(@event => _publishEndpoint.Publish(@event, @event.GetType(), cancellationToken)));

    public Task SchedulePublishAsync(DateTimeOffset scheduledTime, IEvent @event, CancellationToken cancellationToken)
        => _publishEndpoint.CreateMessageScheduler(_bus.Topology).SchedulePublish(scheduledTime.UtcDateTime, @event, @event.GetType(), cancellationToken);
}