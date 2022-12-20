using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Services;

public interface IApplicationService
{
    Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid id, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new();

    Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken);

    Task StreamReplayAsync(string name, CancellationToken cancellationToken);
}