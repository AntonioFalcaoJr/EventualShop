using Application.Abstractions;
using Contracts.Boundaries.Account;

namespace Application.UseCases.Queries;

public class GetAccountInteractor(IProjectionGateway<Projection.AccountDetails> projectionGateway)
    : IInteractor<Query.GetAccountDetails, Projection.AccountDetails>
{
    public Task<Projection.AccountDetails?> InteractAsync(Query.GetAccountDetails query, CancellationToken cancellationToken)
        => projectionGateway.GetAsync(query.AccountId, cancellationToken);
}