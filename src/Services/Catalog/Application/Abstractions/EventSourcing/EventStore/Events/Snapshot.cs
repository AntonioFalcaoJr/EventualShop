using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.EventSourcing.EventStore.Events;

public abstract record Snapshot<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>, new()
    where TId : struct
{
    public long AggregateVersion { get; init; }
    public TId AggregateId { get; init; }
    public string AggregateName { get; } = typeof(TAggregate).Name;
    public TAggregate AggregateState { get; init; } = new();
}