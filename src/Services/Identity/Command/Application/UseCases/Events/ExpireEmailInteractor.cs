using Application.Abstractions;
using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public class ExpireEmailInteractor : EventInteractor<User, DelayedEvent.EmailConfirmationExpired>
{
    public ExpireEmailInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }

    public override Task InteractAsync(DelayedEvent.EmailConfirmationExpired @event, CancellationToken cancellationToken)
        => OnInteractAsync(@event.Id, user => new Command.ExpiryEmail(user.Id, @event.Email), cancellationToken);
}