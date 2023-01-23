using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.EventStore;

public record StoreEvent(long Version, Guid AggregateId, string AggregateName, IEvent DomainEvent, string DomainEventName)
{
    public StoreEvent(IAggregateRoot aggregate, (long version, IEvent @event) tuple)
        : this(tuple.version, aggregate.Id, aggregate.GetType().Name, tuple.@event, tuple.@event.GetType().Name) { }
}