using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;
using DomainEvent = Contracts.Services.Account.DomainEvent;

namespace Application.UseCases.Events;

public class DeleteUserInteractor : IInteractor<DomainEvent.AccountDeleted>
{
    private readonly IApplicationService _service;

    public DeleteUserInteractor(IApplicationService service)
    {
        _service = service;
    }

    public async Task InteractAsync(DomainEvent.AccountDeleted @event, CancellationToken cancellationToken)
    {
        var aggregate = await _service.LoadAggregateAsync<User>(@event.AccountId, cancellationToken);
        aggregate.Handle(new Command.DeleteUser(aggregate.Id));
        await _service.AppendEventsAsync(aggregate, cancellationToken);
    }
}