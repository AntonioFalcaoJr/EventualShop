using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.StoreEvents;

namespace Application.Abstractions.EventStore;

public interface IEventStoreRepository<in TAggregate, in TStoreEvent, TSnapshot>
    where TAggregate : IAggregateRoot, new()
    where TStoreEvent : StoreEvent<TAggregate>
    where TSnapshot : Snapshot<TAggregate>
{
    Task AppendEventsAsync(IEnumerable<TStoreEvent> events, Func<long, CancellationToken, Task> onEventStored, CancellationToken cancellationToken);
    Task AppendEventAsync(TStoreEvent storeEvent, CancellationToken cancellationToken);
    Task AppendSnapshotAsync(TSnapshot snapshot, CancellationToken cancellationToken);
    Task<List<IEvent?>> GetStreamAsync(Guid aggregateId, long version, CancellationToken cancellationToken);
    Task<TSnapshot?> GetSnapshotAsync(Guid aggregateId, CancellationToken cancellationToken);
}