using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;
using Infrastructure.Abstractions.StoreEvents;

namespace Infrastructure.Abstractions.Repositories
{
    public interface IEventStoreRepository<TAggregate, in TStoreEvent, TSnapshot, in TId>
        where TAggregate : IAggregate<TId>, new()
        where TStoreEvent : StoreEvent<TAggregate, TId>
        where TSnapshot : Snapshot<TAggregate, TId>
        where TId : struct
    {
        Task<int> AppendEventToStreamAsync(TStoreEvent @event, CancellationToken cancellationToken);
        Task<IEnumerable<IEvent>> GetStreamByAggregateIdAsync(TId aggregateId, int snapshotVersion, CancellationToken cancellationToken);
        Task AppendSnapshotToStreamAsync(TSnapshot snapshot, CancellationToken cancellationToken);
        Task<TSnapshot> GetSnapshotByAggregateIdAsync(TId aggregateId, CancellationToken cancellationToken);
    }
}