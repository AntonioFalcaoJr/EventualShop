using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.EventStore;

public interface IEventStoreService<in TId, TAggregate>
    where TAggregate : IAggregateRoot<TId>
    where TId : struct
{
    Task AppendEventsAsync(TAggregate aggregate, CancellationToken cancellationToken);
    Task<TAggregate> LoadAggregateAsync(TId aggregateId, CancellationToken cancellationToken);
}