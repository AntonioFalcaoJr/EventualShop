using Application.Abstractions;
using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Domain.Aggregates;
using DomainEvent = Contracts.Services.Account.DomainEvent;

namespace Application.UseCases.Events.Integrations;

public class DeactivateUserWhenAccountDeactivatedInteractor : EventInteractor<User, DomainEvent.AccountDeactivated>
{
    public DeactivateUserWhenAccountDeactivatedInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }

    public override Task InteractAsync(DomainEvent.AccountDeactivated @event, CancellationToken cancellationToken)
        => OnInteractAsync(@event.Id, user => new Command.DeleteUser(user.Id), cancellationToken);
}