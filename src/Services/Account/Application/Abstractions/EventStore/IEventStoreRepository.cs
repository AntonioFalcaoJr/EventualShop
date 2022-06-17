using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.StoreEvents;

namespace Application.Abstractions.EventStore;

public interface IEventStoreRepository<in TAggregate, in TStoreEvent, TSnapshot, in TId>
    where TAggregate : IAggregateRoot<TId, TStoreEvent>
    where TStoreEvent : class, IStoreEvent<TId>
    where TSnapshot : class, ISnapshot<TAggregate, TId>
    where TId : struct
{
    Task AppendEventsAsync(IEnumerable<TStoreEvent> events, Func<long, CancellationToken, Task> onEventStored, CancellationToken cancellationToken);
    Task AppendEventAsync(TStoreEvent storeEvent, CancellationToken cancellationToken);
    Task AppendSnapshotAsync(TSnapshot snapshot, CancellationToken cancellationToken);
    Task<List<IEvent>> GetStreamAsync(TId aggregateId, long version, CancellationToken cancellationToken);
    Task<TSnapshot> GetSnapshotAsync(TId aggregateId, CancellationToken cancellationToken);
}