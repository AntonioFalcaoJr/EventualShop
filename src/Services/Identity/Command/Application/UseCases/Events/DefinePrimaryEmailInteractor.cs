using Application.Abstractions.Interactors;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public class DefinePrimaryEmailInteractor : IInteractor<DomainEvent.EmailConfirmed>
{
    private readonly IApplicationService _applicationService;

    public DefinePrimaryEmailInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(DomainEvent.EmailConfirmed message, CancellationToken cancellationToken)
    {
        var aggregate = await _applicationService.LoadAggregateAsync<User>(message.UserId, cancellationToken);
        aggregate.Handle(new Command.DefinePrimaryEmail(aggregate.Id, message.Email));
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}