using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.StoreEvents;

public abstract record StoreEvent<TId, TAggregate>
    where TAggregate : IAggregateRoot<TId>
    where TId : struct
{
    public long Version { get; }
    public required TId AggregateId { get; init; }
    public string AggregateName { get; } = typeof(TAggregate).Name;
    public required string DomainEventName { get; init; }
    public required IEvent DomainEvent { get; init; }
}