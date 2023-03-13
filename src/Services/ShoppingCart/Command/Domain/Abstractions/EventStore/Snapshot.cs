using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.EventStore;

public record Snapshot(IIdentifier AggregateId, string AggregateType, IAggregateRoot<IIdentifier> Aggregate, long Version, DateTimeOffset Timestamp)
{
    public static Snapshot Create(IAggregateRoot<IIdentifier> aggregate, StoreEvent @event)
        => new(aggregate.Id, aggregate.GetType().Name, aggregate, @event.Version, @event.Timestamp);
}