using Application.Abstractions;
using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Events.Behaviors;

public class DefinePrimaryEmailWhenConfirmedInteractor : EventInteractor<User, DomainEvent.EmailConfirmed>
{
    public DefinePrimaryEmailWhenConfirmedInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }

    public override Task InteractAsync(DomainEvent.EmailConfirmed @event, CancellationToken cancellationToken)
        => OnInteractAsync(@event.Id, user => new Command.ConfirmEmail(user.Id, @event.Email), cancellationToken);
}