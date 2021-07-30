using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Services.EventStore.Events;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;

namespace Application.Abstractions.EventSourcing.Repositories
{
    public interface IEventStoreRepository<TAggregate, in TStoreEvent, TSnapshot, in TId>
        where TAggregate : IAggregateRoot<TId>, new()
        where TStoreEvent : StoreEvent<TAggregate, TId>
        where TSnapshot : Snapshot<TAggregate, TId>
        where TId : struct
    {
        Task<int> AppendEventToStreamAsync(TStoreEvent storeEvent, CancellationToken cancellationToken);
        Task<IEnumerable<IDomainEvent>> GetStreamAsync(TId aggregateId, int snapshotVersion, CancellationToken cancellationToken);
        Task AppendSnapshotToStreamAsync(TSnapshot snapshot, CancellationToken cancellationToken);
        Task<TSnapshot> GetSnapshotAsync(TId aggregateId, CancellationToken cancellationToken);
    }
}