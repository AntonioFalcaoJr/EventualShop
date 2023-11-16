using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public class EmailConfirmedInteractor(IApplicationService service) : IInteractor<DomainEvent.EmailConfirmed>
{
    public async Task InteractAsync(DomainEvent.EmailConfirmed @event, CancellationToken cancellationToken)
    {
        var user = await service.LoadAggregateAsync<User>(@event.UserId, cancellationToken);
        user.Handle(new Command.DefinePrimaryEmail(user.Id, @event.Email));
        await service.AppendEventsAsync(user, cancellationToken);
    }
}