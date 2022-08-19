using Application.Abstractions;
using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Contracts.Services.Account;
using Domain.Aggregates;
using Command = Contracts.Services.Identity.Command;

namespace Application.UseCases;

public class DeactivateUserInteractor : EventInteractor<User, DomainEvent.AccountDeactivated>
{
    public DeactivateUserInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork) 
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }
    
    public override Task InteractAsync(DomainEvent.AccountDeactivated @event, CancellationToken cancellationToken)
        => InteractAsync(@event, root => new Command.DeleteUser(root.Id), cancellationToken);
}