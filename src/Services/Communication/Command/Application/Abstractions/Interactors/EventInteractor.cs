using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.Interactors;

public abstract class EventInteractor<TAggregate, TEvent> : Interactor<TAggregate, TEvent>
    where TAggregate : IAggregateRoot, new()
    where TEvent : IEventWithId
{
    protected EventInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }

    protected async Task OnInteractAsync(Guid aggregateId, Func<TAggregate, ICommandWithId> command, CancellationToken cancellationToken)
    {
        var aggregate = await LoadAggregateAsync(aggregateId, cancellationToken);
        aggregate.Handle(command((TAggregate)aggregate));
        await AppendEventsAsync(aggregate, cancellationToken);
    }
}