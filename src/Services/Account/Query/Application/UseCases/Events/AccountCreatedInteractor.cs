using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public class AccountCreatedInteractor : IInteractor<DomainEvent.AccountCreated>
{
    private readonly IProjectionGateway<Projection.AccountDetails> _projectionGateway;

    public AccountCreatedInteractor(IProjectionGateway<Projection.AccountDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task InteractAsync(DomainEvent.AccountCreated @event, CancellationToken cancellationToken)
        => _projectionGateway.InsertAsync(new(@event.AccountId, false), cancellationToken);
}