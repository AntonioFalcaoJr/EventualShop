using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Services;

public interface IApplicationService
{
    Task<IAggregateRoot> LoadAggregateAsync<TAggregate>(Guid id, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new();

    Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken);

    Task SchedulePublishAsync(DateTimeOffset scheduledTime, IEvent @event, CancellationToken cancellationToken);
}