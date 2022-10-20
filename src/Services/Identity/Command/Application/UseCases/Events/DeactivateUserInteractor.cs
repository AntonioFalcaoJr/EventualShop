using Application.Abstractions.Interactors;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;
using DomainEvent = Contracts.Services.Account.DomainEvent;

namespace Application.UseCases.Events;

public class DeactivateUserInteractor : IInteractor<DomainEvent.AccountDeactivated>
{
    private readonly IApplicationService _applicationService;

    public DeactivateUserInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(DomainEvent.AccountDeactivated message, CancellationToken cancellationToken)
    {
        var aggregate = await _applicationService.LoadAggregateAsync<User>(message.Id, cancellationToken);
        aggregate.Handle(new Command.DeleteUser(aggregate.Id));
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}