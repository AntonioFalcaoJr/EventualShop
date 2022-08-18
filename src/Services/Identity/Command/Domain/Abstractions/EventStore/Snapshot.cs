using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.EventStore;

public record Snapshot(Guid AggregateId, string AggregateName, IAggregateRoot Aggregate, long AggregateVersion);