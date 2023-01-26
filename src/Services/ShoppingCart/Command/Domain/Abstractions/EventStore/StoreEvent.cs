using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.EventStore;

public record StoreEvent(long Version, Guid AggregateId, string AggregateName, IVersionedEvent DomainEvent, string DomainEventName)
{
    public StoreEvent(IAggregateRoot aggregate, IVersionedEvent @event)
        : this(@event.Version, aggregate.Id, aggregate.GetType().Name, @event, @event.GetType().Name) { }
}