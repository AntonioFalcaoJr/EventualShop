using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.StoreEvents;

public abstract record StoreEvent<TAggregate>
    where TAggregate : IAggregateRoot
{
    public long Version { get; init; }
    public Guid AggregateId { get; init; }
    public string AggregateName { get; } = typeof(TAggregate).Name;
    public string? DomainEventName { get; init; }
    public IEvent? DomainEvent { get; init; }
}