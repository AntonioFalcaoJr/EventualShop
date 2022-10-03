using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using MassTransit;

namespace Infrastructure.MessageBus;

public class EventBusGateway : IEventBusGateway
{
    private readonly IPublishEndpoint _publishEndpoint;
    // private readonly IMessageScheduler _messageScheduler;

    public EventBusGateway(IPublishEndpoint publishEndpoint 
        // IMessageScheduler messageScheduler
        )
    {
        _publishEndpoint = publishEndpoint;
        // _messageScheduler = messageScheduler;
    }

    public Task PublishAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken)
        => Task.WhenAll(events.Select(@event => _publishEndpoint.Publish(@event, @event.GetType(), cancellationToken)));

    public Task SchedulePublishAsync(DateTimeOffset scheduledTime, IEvent @event, CancellationToken cancellationToken)
        => Task.CompletedTask
            // _messageScheduler.SchedulePublish(scheduledTime.UtcDateTime, @event, @event.GetType(), cancellationToken)
            ;
}