using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.StoreEvents;

public abstract record StoreEvent<TId, TAggregate>
    where TAggregate : IAggregateRoot<TId>
    where TId : struct
{
    public long Version { get; }
    public TId AggregateId { get; init; }
    public string AggregateName { get; } = typeof(TAggregate).Name;
    public string DomainEventName { get; init; }
    public IEvent DomainEvent { get; init; }
}