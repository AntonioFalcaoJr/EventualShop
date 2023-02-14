using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.EventStore;

public record Snapshot(Guid AggregateId, string AggregateType, IAggregateRoot Aggregate, long Version, DateTimeOffset Timestamp)
{
    public static Snapshot Create(IAggregateRoot aggregate, StoreEvent @event)
        => new(aggregate.Id, aggregate.GetType().Name, aggregate, @event.Version, @event.Timestamp);
}