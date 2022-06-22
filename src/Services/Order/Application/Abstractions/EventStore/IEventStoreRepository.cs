using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.StoreEvents;

namespace Application.Abstractions.EventStore;

public interface IEventStoreRepository<in TAggregate, in TStoreEvent, TSnapshot, in TId>
    where TAggregate : IAggregateRoot<TId>, new()
    where TStoreEvent : StoreEvent<TId, TAggregate>
    where TSnapshot : Snapshot<TId, TAggregate>
    where TId : struct
{
    Task AppendEventsAsync(IEnumerable<TStoreEvent> events, Func<long, CancellationToken, Task> onEventStored, CancellationToken cancellationToken);
    Task AppendEventAsync(TStoreEvent storeEvent, CancellationToken cancellationToken);
    Task AppendSnapshotAsync(TSnapshot snapshot, CancellationToken cancellationToken);
    Task<List<IEvent>> GetStreamAsync(TId aggregateId, long version, CancellationToken cancellationToken);
    Task<TSnapshot> GetSnapshotAsync(TId aggregateId, CancellationToken cancellationToken);
}