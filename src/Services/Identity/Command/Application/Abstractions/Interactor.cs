using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Abstractions;

public abstract class Interactor<TCommand> : IInteractor<TCommand>
    where TCommand : ICommand
{
    private readonly IEventBusGateway _eventBusGateway;
    private readonly IEventStoreGateway _eventStoreGateway;
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

    public virtual async Task InteractAsync<TAggregate, TId>(TCommand command, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>, new()
        where TId : struct
    {
        var aggregate = (TAggregate) await LoadAggregateAsync(Guid.NewGuid(), cancellationToken) ?? new();
        aggregate.Handle(command);
        await AppendEventsAsync(aggregate, cancellationToken);
    }

    protected Task<IAggregateRoot<TId>> LoadAggregateAsync<TId>(TId id, CancellationToken cancellationToken)
        where TId : struct
        => _eventStoreGateway.LoadAsync(id, cancellationToken);

    protected Task AppendEventsAsync<TId>(IAggregateRoot<TId> aggregate, CancellationToken cancellationToken)
        where TId : struct
        => _unitOfWork.ExecuteAsync(async ct =>
        {
            await _eventStoreGateway.AppendAsync(aggregate, ct);
            await _eventBusGateway.PublishAsync(aggregate.Events, ct);
        }, cancellationToken);
}