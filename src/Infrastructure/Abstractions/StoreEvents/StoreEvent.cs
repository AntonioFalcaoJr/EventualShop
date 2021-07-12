using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;

namespace Infrastructure.Abstractions.StoreEvents
{
    public abstract class StoreEvent<TAggregate, TId>
        where TAggregate : IAggregate<TId>
        where TId : struct
    {
        public int Id { get; init; }
        public TId AggregateId { get; init; }
        public string AggregateName { get; } = typeof(TAggregate).Name;
        public string EventName { get; init; }
        public IEvent Event { get; init; }
    }
}