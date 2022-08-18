using Domain.Abstractions.Aggregates;

namespace Application.Abstractions;

public interface IEventStoreGateway
{
    Task AppendAsync(IAggregateRoot aggregate, CancellationToken cancellationToken);
    Task<IAggregateRoot> LoadAsync(Guid aggregateId, CancellationToken cancellationToken);
}