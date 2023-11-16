using Application.Abstractions;
using Contracts.Abstractions.Messages;
using MassTransit;

namespace Infrastructure.EventBus;

public class EventBusGateway(IBus bus, IPublishEndpoint publishEndpoint) : IEventBusGateway
{
    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
        where TEvent : class, IEvent
        => publishEndpoint.Publish(@event, @event.GetType(), cancellationToken);

    public Task SchedulePublishAsync<TEvent>(TEvent @event, DateTimeOffset scheduledTime, CancellationToken cancellationToken)
        where TEvent : class, IDelayedEvent
        => publishEndpoint.CreateMessageScheduler(bus.Topology).SchedulePublish(scheduledTime.UtcDateTime, @event, cancellationToken);
}