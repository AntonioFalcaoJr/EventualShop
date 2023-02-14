using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.EventStore;

public record StoreEvent(Guid AggregateId, string AggregateType, string EventType, IDomainEvent Event, long Version, DateTimeOffset Timestamp)
{
    public static StoreEvent Create<TAggregate, TEvent>(TAggregate aggregate, TEvent @event)
        where TEvent : IDomainEvent
        where TAggregate : IAggregateRoot
        => new(aggregate.Id, typeof(TAggregate).Name, typeof(TEvent).Name, @event, @event.Version, @event.Timestamp);
}