using Contracts.Abstractions.Messages;

namespace Application.Abstractions.Gateways;

public interface IEventBusGateway
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken token)
        where TEvent : class, IEvent;

    Task SchedulePublishAsync<TEvent>(TEvent @event, DateTimeOffset scheduledTime, CancellationToken token)
        where TEvent : class, IDelayedEvent;
}