using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;
using Infrastructure.Abstractions.StoreEvents;

namespace Infrastructure.Abstractions.Repositories
{
    public interface IEventStoreRepository<TAggregate, in TStoreEvent, in TId>
        where TAggregate : IAggregate<TId>, new()
        where TStoreEvent : StoreEvent<TAggregate, TId>, new()
        where TId : struct
    {
        Task AppendEventsToStreamAsync(IEnumerable<TStoreEvent> events, CancellationToken cancellationToken);
        Task<IEnumerable<IEvent>> GetStreamByAggregateId(TId aggregateId, CancellationToken cancellationToken);
    }
}