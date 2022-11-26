using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public class AccountDeletedInteractor : IInteractor<DomainEvent.AccountDeleted>
{
    private readonly IProjectionGateway<Projection.AccountDetails> _projectionGateway;

    public AccountDeletedInteractor(IProjectionGateway<Projection.AccountDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task InteractAsync(DomainEvent.AccountDeleted @event, CancellationToken cancellationToken)
        => _projectionGateway.DeleteAsync(@event.AccountId, cancellationToken);
}