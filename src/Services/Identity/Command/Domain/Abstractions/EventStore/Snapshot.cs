using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.EventStore;

public abstract record Snapshot<TId, TAggregate>
    where TAggregate : IAggregateRoot<TId>, new()
    where TId : struct
{
    public long AggregateVersion { get; init; }
    public TId AggregateId { get; init; }
    public string AggregateName { get; } = typeof(TAggregate).Name;
    public TAggregate AggregateState { get; init; } = new();
}