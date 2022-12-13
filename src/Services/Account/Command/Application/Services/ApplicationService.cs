using Application.Abstractions;
using Contracts.Services.Account;
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

    public Task<IAggregateRoot> LoadAggregateAsync<TAggregate>(Guid id, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new()
        => _eventStoreGateway.LoadAggregateAsync<TAggregate>(id, cancellationToken);

    public Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
        => _unitOfWork.ExecuteAsync(
            operationAsync: async ct =>
            {
                await _eventStoreGateway.AppendEventsAsync(aggregate, ct);
                await _eventBusGateway.PublishAsync(aggregate.Events.Select(tuple => tuple.@event), ct);
            },
            cancellationToken: cancellationToken);

    public async Task StreamReplayAsync(string name, Guid? id, CancellationToken cancellationToken)
    {
        if (id.HasValue)
            await _eventBusGateway.PublishAsync(new NotificationEvent.RebuildProjectionRequested(id.Value, name), cancellationToken);
        else
            await foreach (var accountId in _eventStoreGateway.GetAggregateIdsAsync(cancellationToken))
                await _eventBusGateway.PublishAsync(new NotificationEvent.RebuildProjectionRequested(accountId, name), cancellationToken);
    }
}