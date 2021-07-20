using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.Services
{
    public interface IEventStoreService<TAggregate, in TId>
        where TAggregate : IAggregateRoot<TId>
        where TId : struct
    {
        Task AppendEventsToStreamAsync(TAggregate aggregate, CancellationToken cancellationToken);
        Task<TAggregate> LoadAggregateFromStreamAsync(TId aggregateId, CancellationToken cancellationToken);
    }
}