using Domain.Abstractions;
using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.Gateways;

public interface IEventStoreGateway<TAggregate, TId>
    where TAggregate : IAggregateRoot<IIdentifier>, new()
{
    Task AppendEventsAsync(TAggregate aggregate, CancellationToken cancellationToken);
    Task<TAggregate> LoadAggregateAsync(TId aggregateId, CancellationToken cancellationToken);
    IAsyncEnumerable<TId> StreamAggregatesId();
}