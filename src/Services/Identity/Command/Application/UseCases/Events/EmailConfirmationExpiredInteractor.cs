using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public class EmailConfirmationExpiredInteractor(IApplicationService service) : IInteractor<DelayedEvent.EmailConfirmationExpired>
{
    public async Task InteractAsync(DelayedEvent.EmailConfirmationExpired @event, CancellationToken cancellationToken)
    {
        var user = await service.LoadAggregateAsync<User>(@event.UserId, cancellationToken);
        user.Handle(new Command.ExpiryEmail(user.Id, @event.Email));
        await service.AppendEventsAsync(user, cancellationToken);
    }
}