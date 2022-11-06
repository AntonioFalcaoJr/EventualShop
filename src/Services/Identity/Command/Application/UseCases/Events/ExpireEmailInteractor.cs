using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public class ExpireEmailInteractor : IInteractor<DelayedEvent.EmailConfirmationExpired>
{
    private readonly IApplicationService _service;

    public ExpireEmailInteractor(IApplicationService service)
    {
        _service = service;
    }

    public async Task InteractAsync(DelayedEvent.EmailConfirmationExpired @event, CancellationToken cancellationToken)
    {
        var aggregate = await _service.LoadAggregateAsync<User>(@event.UserId, cancellationToken);
        aggregate.Handle(new Command.ExpiryEmail(aggregate.Id, @event.Email));
        await _service.AppendEventsAsync(aggregate, cancellationToken);
    }
}