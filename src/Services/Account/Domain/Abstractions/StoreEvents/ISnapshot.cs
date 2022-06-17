using Domain.Abstractions.Aggregates;

namespace Domain.Abstractions.StoreEvents;

public interface ISnapshot<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId, IStoreEvent<TId>>
    where TId : struct
{
    TId AggregateId { get; init; }
    long AggregateVersion { get; init; }
    TAggregate AggregateState { get; init; }
}