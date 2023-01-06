using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.Gateways;

public interface IEventStoreGateway
{
    Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken);

    Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new();
    
    IAsyncEnumerable<Guid> StreamAggregatesId(CancellationToken cancellationToken);
}