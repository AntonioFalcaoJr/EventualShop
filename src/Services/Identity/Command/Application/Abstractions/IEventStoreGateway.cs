using Domain.Abstractions.Aggregates;

namespace Application.Abstractions;

public interface IEventStoreGateway
{
    Task AppendAsync(IAggregateRoot aggregate, CancellationToken cancellationToken);
    Task<IAggregateRoot> LoadAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new();
}