using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public class EmailConfirmedInteractor : IInteractor<DomainEvent.EmailConfirmed>
{
    private readonly IApplicationService _applicationService;

    public EmailConfirmedInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(DomainEvent.EmailConfirmed @event, CancellationToken cancellationToken)
    {
        var user = await _applicationService.LoadAggregateAsync<User>(@event.UserId, cancellationToken);
        user.Handle(new Command.DefinePrimaryEmail(user.Id, @event.Email));
        await _applicationService.AppendEventsAsync(user, cancellationToken);
    }
}