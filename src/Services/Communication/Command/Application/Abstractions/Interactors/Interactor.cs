using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.Interactors;

public abstract class Interactor<TAggregate, TMessage> : IInteractor<TMessage>
    where TAggregate : IAggregateRoot, new()
    where TMessage : IMessage
{
    private readonly IEventStoreGateway _eventStoreGateway;
    private readonly IEventBusGateway _eventBusGateway;
    private readonly IUnitOfWork _unitOfWork;

    protected Interactor(
        IEventStoreGateway eventStoreGateway,
        IEventBusGateway eventBusGateway,
        IUnitOfWork unitOfWork)
    {
        _eventStoreGateway = eventStoreGateway;
        _eventBusGateway = eventBusGateway;
        _unitOfWork = unitOfWork;
    }

    public abstract Task InteractAsync(TMessage message, CancellationToken cancellationToken);

    protected Task<IAggregateRoot> LoadAggregateAsync(Guid id, CancellationToken cancellationToken)
        => _eventStoreGateway.LoadAsync<TAggregate>(id, cancellationToken);

    protected Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
        => _unitOfWork.ExecuteAsync(async ct =>
        {
            await _eventStoreGateway.AppendAsync(aggregate, ct);
            await _eventBusGateway.PublishAsync(aggregate.Events.Select(tuple => tuple.@event), ct);
        }, cancellationToken);
}