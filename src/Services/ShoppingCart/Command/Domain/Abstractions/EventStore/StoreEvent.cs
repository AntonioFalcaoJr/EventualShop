using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.EventStore;

public record StoreEvent(Guid AggregateId, string AggregateType, string EventType, IDomainEvent Event, long Version, DateTimeOffset Timestamp)
{
    public StoreEvent(IAggregateRoot aggregateRoot, IDomainEvent @event)
        : this(aggregateRoot.Id, aggregateRoot.GetType().Name, @event.GetType().Name, @event, @event.Version, @event.Timestamp) { }
}