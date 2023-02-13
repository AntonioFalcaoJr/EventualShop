using Application.Abstractions;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Services;

public class ApplicationService : IApplicationService
{
    private readonly IEventStoreGateway _eventStoreGateway;
    private readonly IEventBusGateway _eventBusGateway;

    public ApplicationService(
        IEventStoreGateway eventStoreGateway,
        IEventBusGateway eventBusGateway)
    {
        _eventStoreGateway = eventStoreGateway;
        _eventBusGateway = eventBusGateway;
    }

    public Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid id, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new()
        => _eventStoreGateway.LoadAggregateAsync<TAggregate>(id, cancellationToken);

    public Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
        => _eventStoreGateway.ExecuteTransactionAsync(
            operationAsync: async ct =>
            {
                await _eventStoreGateway.AppendEventsAsync(aggregate, ct);
                await _eventBusGateway.PublishAsync(aggregate.UncommittedEvents, ct);
            },
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<Guid> StreamAggregatesId(CancellationToken cancellationToken)
        => _eventStoreGateway.StreamAggregatesId(cancellationToken);

    public Task PublishEventAsync(IEvent @event, CancellationToken cancellationToken)
        => _eventBusGateway.PublishAsync(@event, cancellationToken);
}