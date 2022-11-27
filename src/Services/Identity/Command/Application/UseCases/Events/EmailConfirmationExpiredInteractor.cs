using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public class EmailConfirmationExpiredInteractor : IInteractor<DelayedEvent.EmailConfirmationExpired>
{
    private readonly IApplicationService _applicationService;

    public EmailConfirmationExpiredInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(DelayedEvent.EmailConfirmationExpired @event, CancellationToken cancellationToken)
    {
        var aggregate = await _applicationService.LoadAggregateAsync<User>(@event.UserId, cancellationToken);
        aggregate.Handle(new Command.ExpiryEmail(aggregate.Id, @event.Email));
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}