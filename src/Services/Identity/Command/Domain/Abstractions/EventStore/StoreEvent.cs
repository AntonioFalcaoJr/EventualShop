using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.EventStore;

public record StoreEvent(long Version, Guid AggregateId, string AggregateName, IEvent DomainEvent, string DomainEventName)
{
    public StoreEvent(IEvent domainEvent, IAggregateRoot aggregate)
        : this(aggregate.Version, aggregate.Id, aggregate.Name, domainEvent, domainEvent.Name) { }
};