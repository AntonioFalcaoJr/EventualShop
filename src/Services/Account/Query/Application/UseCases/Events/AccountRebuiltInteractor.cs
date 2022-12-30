using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public class AccountRebuiltInteractor : IInteractor<IntegrationEvent.ProjectionRebuilt>
{
    private readonly IProjectionGateway<Projection.AccountDetails> _projectionGateway;

    public AccountRebuiltInteractor(IProjectionGateway<Projection.AccountDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(IntegrationEvent.ProjectionRebuilt @event, CancellationToken cancellationToken)
    {
        Projection.AccountDetails accountDetails =
            new(@event.AccountId,
                @event.Account.Profile.FirstName,
                @event.Account.Profile.LastName,
                @event.Account.Profile.Email,
                false);

        await _projectionGateway.ReplaceAsync(accountDetails, cancellationToken);
    }
}
