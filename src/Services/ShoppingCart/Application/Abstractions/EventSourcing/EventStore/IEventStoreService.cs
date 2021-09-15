using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.EventSourcing.EventStore
{
    public interface IEventStoreService<TAggregate, in TId>
        where TAggregate : IAggregateRoot<TId>
        where TId : struct
    {
        Task AppendEventsToStreamAsync(TAggregate aggregateState, CancellationToken cancellationToken);
        Task<TAggregate> LoadAggregateFromStreamAsync(TId aggregateId, CancellationToken cancellationToken);
    }
}