using Domain.Abstractions.Aggregates;
using Messages.Abstractions;

namespace Application.Abstractions.EventSourcing.EventStore.Events
{
    public abstract record StoreEvent<TAggregate, TId>
        where TAggregate : IAggregateRoot<TId>
        where TId : struct
    {
        public int Version { get; }
        public TId AggregateId { get; init; }
        public string AggregateName { get; } = typeof(TAggregate).Name;
        public string EventName { get; init; }
        public IEvent Event { get; init; }
    }
}