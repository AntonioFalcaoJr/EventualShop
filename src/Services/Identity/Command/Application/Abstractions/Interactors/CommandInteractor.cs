using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.Interactors;

public abstract class CommandInteractor<TAggregate, TCommand> : ICommandInteractor<TCommand>
    where TAggregate : IAggregateRoot, new()
    where TCommand : ICommandWithId
{
    private readonly IEventStoreGateway _eventStoreGateway;
    private readonly IEventBusGateway _eventBusGateway;
    private readonly IUnitOfWork _unitOfWork;

    protected CommandInteractor(
        IEventStoreGateway eventStoreGateway,
        IEventBusGateway eventBusGateway,
        IUnitOfWork unitOfWork)
    {
        _eventStoreGateway = eventStoreGateway;
        _eventBusGateway = eventBusGateway;
        _unitOfWork = unitOfWork;
    }

    public virtual async Task InteractAsync(TCommand command, CancellationToken cancellationToken)
    {
        var aggregate = await LoadAggregateAsync(command.Id, cancellationToken);
        aggregate.Handle(command);
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