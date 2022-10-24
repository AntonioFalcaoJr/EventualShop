using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.EventStore;

public interface IEventStoreService<TAggregate>
    where TAggregate : IAggregateRoot
{
    Task AppendAsync(TAggregate aggregate, CancellationToken cancellationToken);
    Task<TAggregate> LoadAsync(Guid aggregateId, CancellationToken cancellationToken);
}