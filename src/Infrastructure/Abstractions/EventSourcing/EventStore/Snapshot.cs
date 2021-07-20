using Domain.Abstractions.Aggregates;

namespace Infrastructure.Abstractions.EventSourcing.EventStore
{
    public abstract record Snapshot<TAggregate, TId>
        where TAggregate : IAggregateRoot<TId>, new()
        where TId : struct
    {
        public int Version { get; init; }
        public TId AggregateId { get; init; }
        public string AggregateName { get; } = typeof(TAggregate).Name;
        public TAggregate Aggregate { get; init; } = new();
    }
}