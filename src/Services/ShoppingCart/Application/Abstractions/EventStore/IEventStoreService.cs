using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.EventStore;

public interface IEventStoreService<in TId, TAggregate>
    where TAggregate : IAggregateRoot<TId>
    where TId : struct
{
    Task AppendAsync(TAggregate aggregate, CancellationToken cancellationToken);
    Task<TAggregate> LoadAsync(TId aggregateId, CancellationToken cancellationToken);
    IAsyncEnumerable<TAggregate> LoadAggregatesAsync(CancellationToken cancellationToken);
}