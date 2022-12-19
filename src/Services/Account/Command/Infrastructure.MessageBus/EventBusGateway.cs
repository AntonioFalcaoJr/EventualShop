using Application.Abstractions;
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

    public async Task SendAsync(IEvent @event, string exchange, CancellationToken cancellationToken)
    {
        var endpoint = await _bus.GetSendEndpoint(new(exchange));
        await endpoint.Send(@event, cancellationToken);
    }

    public Task PublishAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken)
    => Task.WhenAll(events.Select(@event => _publishEndpoint.Publish(@event, @event.GetType(), cancellationToken)));

    public Task PublishAsync(IEvent @event, CancellationToken cancellationToken)
        => _publishEndpoint.Publish(@event, @event.GetType(), cancellationToken);

    public Task SchedulePublishAsync(DateTimeOffset scheduledTime, IEvent @event, CancellationToken cancellationToken)
        => _publishEndpoint.CreateMessageScheduler(_bus.Topology).SchedulePublish(scheduledTime.UtcDateTime, @event, @event.GetType(), cancellationToken);

}