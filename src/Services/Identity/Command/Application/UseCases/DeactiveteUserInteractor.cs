using Application.Abstractions;
using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Domain.Aggregates;
using DomainEvent = Contracts.Services.Account.DomainEvent;

namespace Application.UseCases;

public class DeactivateUserInteractor : EventInteractor<User, DomainEvent.AccountDeactivated>
{
    public DeactivateUserInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }

    public override Task InteractAsync(DomainEvent.AccountDeactivated @event, CancellationToken cancellationToken)
        => OnInteractAsync(@event, user => new Command.DeleteUser(user.Id), cancellationToken);
}