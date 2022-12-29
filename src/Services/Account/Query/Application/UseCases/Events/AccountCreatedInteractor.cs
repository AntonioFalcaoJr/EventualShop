using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public class AccountCreatedInteractor : IInteractor<DomainEvent.AccountCreated>,
{
    private readonly IProjectionGateway<Projection.AccountDetails> _projectionGateway;

    public AccountCreatedInteractor(IProjectionGateway<Projection.AccountDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.AccountCreated @event, CancellationToken cancellationToken)
    {
        Projection.AccountDetails accountDetails =
            new(@event.AccountId,
                @event.FirstName,
                @event.LastName,
                @event.Email,
                false);

        await _projectionGateway.InsertAsync(accountDetails, cancellationToken);
    }
}