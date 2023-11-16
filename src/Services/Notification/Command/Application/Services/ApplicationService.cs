using Application.Abstractions;
using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Services;

public class ApplicationService(IEventStoreGateway eventStoreGateway,
        IEventBusGateway eventBusGateway,
        IUnitOfWork unitOfWork)
    : IApplicationService
{
    public Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid id, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new()
        => eventStoreGateway.LoadAggregateAsync<TAggregate>(id, cancellationToken);

    public Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
        => unitOfWork.ExecuteAsync(
            operationAsync: async ct =>
            {
                await eventStoreGateway.AppendEventsAsync(aggregate, ct);
                await eventBusGateway.PublishAsync(aggregate.UncommittedEvents, ct);
            },
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<Guid> StreamAggregatesId()
        => eventStoreGateway.StreamAggregatesId();

    public Task PublishEventAsync(IEvent @event, CancellationToken cancellationToken)
        => eventBusGateway.PublishAsync(@event, cancellationToken);

    public Task SchedulePublishAsync(IDelayedEvent @event, DateTimeOffset scheduledTime, CancellationToken cancellationToken)
        => eventBusGateway.SchedulePublishAsync(@event, scheduledTime, cancellationToken);
}