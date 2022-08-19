using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.Interactors;

public abstract class EventInteractor<TAggregate, TEvent> : IEventInteractor<TEvent>
    where TAggregate : IAggregateRoot, new()
    where TEvent : IEventWithId
{
    private readonly IEventStoreGateway _eventStoreGateway;
    private readonly IEventBusGateway _eventBusGateway;
    private readonly IUnitOfWork _unitOfWork;

    protected EventInteractor(
        IEventStoreGateway eventStoreGateway,
        IEventBusGateway eventBusGateway,
        IUnitOfWork unitOfWork)
    {
        _eventStoreGateway = eventStoreGateway;
        _eventBusGateway = eventBusGateway;
        _unitOfWork = unitOfWork;
    }

    public abstract Task InteractAsync(TEvent @event, CancellationToken cancellationToken);

    protected async Task InteractAsync(TEvent @event, Func<IAggregateRoot, ICommandWithId> command, CancellationToken cancellationToken)
    {
        var aggregate = await LoadAggregateAsync(@event.Id, cancellationToken);
        aggregate.Handle(command(aggregate));
        await AppendEventsAsync(aggregate, cancellationToken);
    }

    protected Task<IAggregateRoot> LoadAggregateAsync(Guid id, CancellationToken cancellationToken)
        => _eventStoreGateway.LoadAsync<TAggregate>(id, cancellationToken);

    protected Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
        => _unitOfWork.ExecuteAsync(async ct =>
        {
            await _eventStoreGateway.AppendAsync(aggregate, ct);
            await _eventBusGateway.PublishAsync(aggregate.Events, ct);
        }, cancellationToken);
}