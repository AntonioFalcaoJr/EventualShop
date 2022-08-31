using Contracts.Abstractions.Messages;

namespace Domain.Abstractions.EventStore;

public interface IEventStoreRepository
{
    Task AppendEventsAsync(IEnumerable<StoreEvent> events, Func<long, CancellationToken, Task> onEventStored, CancellationToken cancellationToken);
    Task AppendEventAsync(StoreEvent storeEvent, CancellationToken cancellationToken);
    Task AppendSnapshotAsync(Snapshot snapshot, CancellationToken cancellationToken);
    Task<List<IEvent>> GetStreamAsync(Guid aggregateId, long version, CancellationToken cancellationToken);
    Task<Snapshot> GetSnapshotAsync(Guid aggregateId, CancellationToken cancellationToken);
}