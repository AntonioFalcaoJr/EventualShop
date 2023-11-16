using Contracts.Abstractions.Messages;

namespace Application.Abstractions.Gateways;

public interface IEventBusGateway
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
        where TEvent : class, IEvent;

    Task SchedulePublishAsync<TEvent>(TEvent @event, DateTimeOffset scheduledTime, CancellationToken cancellationToken)
        where TEvent : class, IDelayedEvent;
}