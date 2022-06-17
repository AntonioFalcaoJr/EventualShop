using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.StoreEvents;

public abstract record Snapshot<TAggregate, TId> : ISnapshot<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId, IStoreEvent<TId>>, new()
    where TId : struct
{
    public long AggregateVersion { get; init; }
    public TId AggregateId { get; init; }
    public string AggregateName { get; } = typeof(TAggregate).Name;
    public TAggregate AggregateState { get; init; } = new();
}