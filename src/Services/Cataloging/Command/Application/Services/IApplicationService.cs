using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Identities;

namespace Application.Services;

public interface IApplicationService
{
    Task AppendEventsAsync<TAggregate, TId>(TAggregate aggregate, CancellationToken token)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new();

    Task AppendEventsAsync<TAggregate, TId>(TAggregate aggregate, string referenceId, CancellationToken token)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new();

    Task<TAggregate> LoadAggregateAsync<TAggregate, TId>(TId id, CancellationToken token)
        where TAggregate : class, IAggregateRoot<TId>, new()
        where TId : IIdentifier, new();

    Task<TAggregate> LoadAggregateByReferenceIdAsync<TAggregate, TId>(string referenceId, CancellationToken token)
        where TAggregate : class, IAggregateRoot<TId>, new()
        where TId : IIdentifier, new();

    IAsyncEnumerable<TId> StreamAggregatesId<TAggregate, TId>()
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new();

    Task PublishEventAsync(IEvent @event, CancellationToken token);

    Task SchedulePublishAsync(IDelayedEvent @event, DateTimeOffset scheduledTime, CancellationToken token);
}