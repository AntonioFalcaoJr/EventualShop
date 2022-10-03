using Application.Abstractions.Gateways;
using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;

namespace Application.Abstractions.Interactors;

public abstract class CommandInteractor<TAggregate, TCommand> : Interactor<TAggregate, TCommand>
    where TAggregate : IAggregateRoot, new()
    where TCommand : ICommand
{
    protected CommandInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }

    public override async Task InteractAsync(TCommand command, CancellationToken cancellationToken)
    {
        var aggregate = await LoadAggregateAsync(command.Id, cancellationToken);
        aggregate.Handle(command);
        await AppendEventsAsync(aggregate, cancellationToken);
    }
}