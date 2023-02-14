using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.EventStore;

public record Snapshot(Guid AggregateId, string AggregateType, IAggregateRoot Aggregate, long Version, DateTimeOffset Timestamp)
{
    public static Snapshot Create<TAggregate>(TAggregate aggregate, StoreEvent @event)
        where TAggregate : IAggregateRoot
        => new(aggregate.Id, typeof(TAggregate).Name, aggregate, @event.Version, @event.Timestamp);
}