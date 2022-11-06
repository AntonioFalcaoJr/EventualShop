using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;
using DomainEvent = Contracts.Services.Account.DomainEvent;

namespace Application.UseCases.Events;

public class DeactivateUserInteractor : IInteractor<DomainEvent.AccountDeactivated>
{
    private readonly IApplicationService _service;

    public DeactivateUserInteractor(IApplicationService service)
    {
        _service = service;
    }

    public async Task InteractAsync(DomainEvent.AccountDeactivated @event, CancellationToken cancellationToken)
    {
        var aggregate = await _service.LoadAggregateAsync<User>(@event.AccountId, cancellationToken);
        aggregate.Handle(new Command.DeleteUser(aggregate.Id));
        await _service.AppendEventsAsync(aggregate, cancellationToken);
    }
}