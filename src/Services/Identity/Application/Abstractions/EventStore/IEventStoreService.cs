using Domain.Abstractions.Aggregates;
using Domain.Abstractions.StoreEvents;

namespace Application.Abstractions.EventStore;

public interface IEventStoreService<TAggregate, in TId>
    where TAggregate : IAggregateRoot<TId, IStoreEvent<TId>>
    where TId : struct
{
    Task AppendEventsAsync(TAggregate aggregate, CancellationToken cancellationToken);
    Task<TAggregate> LoadAggregateAsync(TId aggregateId, CancellationToken cancellationToken);
}