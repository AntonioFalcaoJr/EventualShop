using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;
using DomainEvent = Contracts.Services.Account.DomainEvent;

namespace Application.UseCases.Events;

public class AccountDeactivatedInteractor : IInteractor<DomainEvent.AccountDeactivated>
{
    private readonly IApplicationService _applicationService;

    public AccountDeactivatedInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(DomainEvent.AccountDeactivated @event, CancellationToken cancellationToken)
    {
        var aggregate = await _applicationService.LoadAggregateAsync<User>(@event.AccountId, cancellationToken);
        aggregate.Handle(new Command.DeleteUser(aggregate.Id));
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}