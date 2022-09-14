using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.Interactors;

public abstract class Interactor<TAggregate, TMessage> : IInteractor<TMessage>
    where TAggregate : IAggregateRoot, new()
    where TMessage : IMessage
{
    protected readonly IEventStoreGateway EventStoreGateway;
    protected readonly IEventBusGateway EventBusGateway;
    protected readonly IUnitOfWork UnitOfWork;

    protected Interactor(
        IEventStoreGateway eventStoreGateway,
        IEventBusGateway eventBusGateway,
        IUnitOfWork unitOfWork)
    {
        EventStoreGateway = eventStoreGateway;
        EventBusGateway = eventBusGateway;
        UnitOfWork = unitOfWork;
    }

    public abstract Task InteractAsync(TMessage message, CancellationToken cancellationToken);

    protected Task<IAggregateRoot> LoadAggregateAsync(Guid id, CancellationToken cancellationToken)
        => EventStoreGateway.LoadAsync<TAggregate>(id, cancellationToken);

    protected Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
        => UnitOfWork.ExecuteAsync(async ct =>
        {
            await EventStoreGateway.AppendAsync(aggregate, ct);
            await EventBusGateway.PublishAsync(aggregate.Events, ct);
        }, cancellationToken);
}