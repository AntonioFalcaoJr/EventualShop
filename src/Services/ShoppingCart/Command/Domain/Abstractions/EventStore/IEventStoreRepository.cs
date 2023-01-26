using Contracts.Abstractions.Messages;

namespace Domain.Abstractions.EventStore;

public interface IEventStoreRepository
{
    Task AppendEventAsync(StoreEvent storeEvent, CancellationToken cancellationToken);
    Task AppendSnapshotAsync(Snapshot snapshot, CancellationToken cancellationToken);
    Task<List<IVersionedEvent>> GetStreamAsync(Guid aggregateId, long? version, CancellationToken cancellationToken);
    Task<Snapshot?> GetSnapshotAsync(Guid aggregateId, CancellationToken cancellationToken);
    IAsyncEnumerable<Guid> GetAggregateIdsAsync(CancellationToken cancellationToken);
}