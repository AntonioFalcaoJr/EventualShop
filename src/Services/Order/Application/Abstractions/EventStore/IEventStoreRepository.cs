using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.StoreEvents;

namespace Application.Abstractions.EventStore;

public interface IEventStoreRepository<TAggregate, TStoreEvent, TSnapshot, in TId>
    where TAggregate : IAggregateRoot<TId>, new()
    where TStoreEvent : StoreEvent<TAggregate, TId>
    where TSnapshot : Snapshot<TAggregate, TId>
    where TId : struct
{
    Task AppendEventsAsync(IEnumerable<TStoreEvent> events, Func<TStoreEvent, CancellationToken, Task> onEventStored, CancellationToken cancellationToken);
    Task<List<IEvent>> GetStreamAsync(TId aggregateId, long version, CancellationToken cancellationToken);
    Task AppendSnapshotAsync(TSnapshot snapshot, CancellationToken cancellationToken);
    Task<TSnapshot> GetSnapshotAsync(TId aggregateId, CancellationToken cancellationToken);
}