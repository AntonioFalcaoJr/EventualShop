using Application.Abstractions;
using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public class DefinePrimaryEmailInteractor : EventInteractor<User, DomainEvent.EmailConfirmed>
{
    public DefinePrimaryEmailInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }

    public override Task InteractAsync(DomainEvent.EmailConfirmed @event, CancellationToken cancellationToken)
        => OnInteractAsync(@event.Id, user => new Command.DefinePrimaryEmail(user.Id, @event.Email), cancellationToken);
}