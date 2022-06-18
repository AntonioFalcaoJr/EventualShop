using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.StoreEvents;

public abstract record Snapshot<TId, TAggregate> 
    where TAggregate : IAggregateRoot<TId>, new()
    where TId : struct
{
    public required long AggregateVersion { get; init; }
    public required TId AggregateId { get; init; }
    public string AggregateName { get; } = typeof(TAggregate).Name;
    public required TAggregate AggregateState { get; init; } = new();
}