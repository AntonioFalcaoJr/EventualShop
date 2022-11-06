using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Events;

public class DefinePrimaryEmailInteractor : IInteractor<DomainEvent.EmailConfirmed>
{
    private readonly IApplicationService _service;

    public DefinePrimaryEmailInteractor(IApplicationService service)
    {
        _service = service;
    }

    public async Task InteractAsync(DomainEvent.EmailConfirmed @event, CancellationToken cancellationToken)
    {
        var aggregate = await _service.LoadAggregateAsync<User>(@event.UserId, cancellationToken);
        aggregate.Handle(new Command.DefinePrimaryEmail(aggregate.Id, @event.Email));
        await _service.AppendEventsAsync(aggregate, cancellationToken);
    }
}