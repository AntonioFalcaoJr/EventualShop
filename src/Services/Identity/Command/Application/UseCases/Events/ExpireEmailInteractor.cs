using Application.Abstractions.Interactors;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public class ExpireEmailInteractor : IInteractor<DelayedEvent.EmailConfirmationExpired>
{
    private readonly IApplicationService _applicationService;

    public ExpireEmailInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(DelayedEvent.EmailConfirmationExpired message, CancellationToken cancellationToken)
    {
        var aggregate = await _applicationService.LoadAggregateAsync<User>(message.Id, cancellationToken);
        aggregate.Handle(new Command.ExpiryEmail(aggregate.Id, message.Email));
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}