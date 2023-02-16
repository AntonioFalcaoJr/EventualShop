using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;
using DomainEvent = Contracts.Services.Account.DomainEvent;

namespace Application.UseCases.Events;

public class AccountDeletedInteractor : IInteractor<DomainEvent.AccountDeleted>
{
    private readonly IApplicationService _applicationService;

    public AccountDeletedInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(DomainEvent.AccountDeleted @event, CancellationToken cancellationToken)
    {
        var user = await _applicationService.LoadAggregateAsync<User>(@event.AccountId, cancellationToken);
        user.Handle(new Command.DeleteUser(user.Id));
        await _applicationService.AppendEventsAsync(user, cancellationToken);
    }
}