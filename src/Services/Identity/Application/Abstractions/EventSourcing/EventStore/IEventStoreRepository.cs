using Application.Abstractions.EventSourcing.EventStore.Events;
using Domain.Abstractions.Aggregates;
using ECommerce.Abstractions.Messages.Events;

namespace Application.Abstractions.EventSourcing.EventStore;

public interface IEventStoreRepository<TAggregate, in TStoreEvent, TSnapshot, in TId>
    where TAggregate : IAggregateRoot<TId>, new()
    where TStoreEvent : StoreEvent<TAggregate, TId>
    where TSnapshot : Snapshot<TAggregate, TId>
    where TId : struct
{
    Task<int> AppendEventToStreamAsync(TStoreEvent storeEvent, CancellationToken cancellationToken);
    Task<IEnumerable<IEvent>> GetStreamAsync(TId aggregateId, int snapshotVersion, CancellationToken cancellationToken);
    Task AppendSnapshotToStreamAsync(TSnapshot snapshot, CancellationToken cancellationToken);
    Task<TSnapshot> GetSnapshotAsync(TId aggregateId, CancellationToken cancellationToken);
}