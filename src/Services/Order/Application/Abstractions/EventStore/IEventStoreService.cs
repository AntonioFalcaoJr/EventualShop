using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.EventStore;

public interface IEventStoreService<TAggregate, in TId>
    where TAggregate : IAggregateRoot<TId>
    where TId : struct
{
    Task AppendEventsAsync(TAggregate aggregateState, CancellationToken cancellationToken);
    Task<TAggregate> LoadAggregateAsync(TId aggregateId, CancellationToken cancellationToken);
}