using Application.Abstractions;
using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Services;

public class ApplicationService : IApplicationService
{
    private readonly IEventStoreGateway _eventStoreGateway;
    private readonly IEventBusGateway _eventBusGateway;
    private readonly IUnitOfWork _unitOfWork;

    public ApplicationService(
        IEventStoreGateway eventStoreGateway,
        IEventBusGateway eventBusGateway,
        IUnitOfWork unitOfWork)
    {
        _eventStoreGateway = eventStoreGateway;
        _eventBusGateway = eventBusGateway;
        _unitOfWork = unitOfWork;
    }

    public Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid id, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new()
        => _eventStoreGateway.LoadAggregateAsync<TAggregate>(id, cancellationToken);

    public Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
        => _unitOfWork.ExecuteAsync(
            operationAsync: async ct =>
            {
                await _eventStoreGateway.AppendEventsAsync(aggregate, ct);
                await _eventBusGateway.PublishAsync(aggregate.UncommittedEvents, ct);
            },
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<Guid> StreamAggregatesId()
        => _eventStoreGateway.StreamAggregatesId();

    public Task PublishEventAsync(IEvent @event, CancellationToken cancellationToken)
        => _eventBusGateway.PublishAsync(@event, cancellationToken);

    public Task SchedulePublishAsync(IDelayedEvent @event, DateTimeOffset scheduledTime, CancellationToken cancellationToken)
        => _eventBusGateway.SchedulePublishAsync(@event, scheduledTime, cancellationToken);
}