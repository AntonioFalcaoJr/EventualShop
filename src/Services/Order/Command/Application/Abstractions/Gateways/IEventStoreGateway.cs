using System.Runtime.CompilerServices;
using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.Gateways;

public interface IEventStoreGateway
{
    Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken);
    Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken) where TAggregate : IAggregateRoot, new();
    ConfiguredCancelableAsyncEnumerable<Guid> StreamAggregatesId(CancellationToken cancellationToken);
    Task ExecuteTransactionAsync(Func<CancellationToken, Task> operationAsync, CancellationToken cancellationToken);
}