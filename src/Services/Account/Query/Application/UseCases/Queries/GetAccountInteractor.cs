using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Queries;

public class GetAccountInteractor : IInteractor<Query.GetAccount, Projection.AccountDetails>
{
    private readonly IProjectionGateway<Projection.AccountDetails> _projectionGateway;

    public GetAccountInteractor(IProjectionGateway<Projection.AccountDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<Projection.AccountDetails?> InteractAsync(Query.GetAccount query, CancellationToken cancellationToken)
        => _projectionGateway.GetAsync(query.AccountId, cancellationToken);
}