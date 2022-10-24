using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.StoreEvents;

public abstract record Snapshot<TAggregate>
    where TAggregate : IAggregateRoot, new()
{
    public long AggregateVersion { get; init; }
    public Guid AggregateId { get; init; }
    public string AggregateName { get; } = typeof(TAggregate).Name;
    public TAggregate AggregateState { get; init; } = new();
}