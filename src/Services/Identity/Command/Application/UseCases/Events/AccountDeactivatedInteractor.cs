using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Identity;
using Domain.Aggregates;
using DomainEvent = Contracts.Boundaries.Account.DomainEvent;

namespace Application.UseCases.Events;

public class AccountDeactivatedInteractor(IApplicationService service) : IInteractor<DomainEvent.AccountDeactivated>
{
    public async Task InteractAsync(DomainEvent.AccountDeactivated @event, CancellationToken cancellationToken)
    {
        var user = await service.LoadAggregateAsync<User>(@event.AccountId, cancellationToken);
        user.Handle(new Command.DeleteUser(user.Id));
        await service.AppendEventsAsync(user, cancellationToken);
    }
}