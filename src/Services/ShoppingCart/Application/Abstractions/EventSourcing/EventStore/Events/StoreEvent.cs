using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;

namespace Application.Abstractions.EventSourcing.EventStore.Events
{
    public abstract record StoreEvent<TAggregate, TId>
        where TAggregate : IAggregateRoot<TId>
        where TId : struct
    {
        public int Version { get; }
        public TId AggregateId { get; init; }
        public string AggregateName { get; } = typeof(TAggregate).Name;
        public string DomainEventName { get; init; }
        public IDomainEvent DomainEvent { get; init; }
    }
}