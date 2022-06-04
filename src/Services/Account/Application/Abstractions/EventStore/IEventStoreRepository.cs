using Contracts.Abstractions;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.StoreEvents;

namespace Application.Abstractions.EventStore;

public interface IEventStoreRepository<TAggregate, in TStoreEvent, TSnapshot, in TId>
    where TAggregate : IAggregateRoot<TId>, new()
    where TStoreEvent : StoreEvent<TAggregate, TId>
    where TSnapshot : Snapshot<TAggregate, TId>
    where TId : struct
{
    Task<long> AppendEventsAsync(TStoreEvent storeEvent, CancellationToken cancellationToken);
    Task<IEnumerable<IEvent>> GetStreamAsync(TId aggregateId, long version, CancellationToken cancellationToken);
    Task AppendSnapshotAsync(TSnapshot snapshot, CancellationToken cancellationToken);
    Task<TSnapshot> GetSnapshotAsync(TId aggregateId, CancellationToken cancellationToken);
}