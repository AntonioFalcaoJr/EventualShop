using Application.Abstractions.Interactors;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;
using DomainEvent = Contracts.Services.Account.DomainEvent;

namespace Application.UseCases.Events;

public class DeleteUserInteractor : IInteractor<DomainEvent.AccountDeleted>
{
    private readonly IApplicationService _applicationService;

    public DeleteUserInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(DomainEvent.AccountDeleted message, CancellationToken cancellationToken)
    {
        var aggregate = await _applicationService.LoadAggregateAsync<User>(message.AccountId, cancellationToken);
        aggregate.Handle(new Command.DeleteUser(aggregate.Id));
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}