using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Services;

public interface IApplicationService
{
    Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken);
    Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid id, CancellationToken cancellationToken) where TAggregate : IAggregateRoot, new();
    IAsyncEnumerable<Guid> StreamAggregatesId(CancellationToken cancellationToken);
    Task PublishEventAsync(IEvent @event, CancellationToken cancellationToken);
    Task SchedulePublishAsync(IDelayedEvent @event, DateTimeOffset scheduledTime, CancellationToken cancellationToken);
}