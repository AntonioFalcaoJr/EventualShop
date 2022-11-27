using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Account;

namespace Application.UseCases.Queries;

public class ListAccountsInteractor : IInteractor<Query.ListAccounts, IPagedResult<Projection.AccountDetails>>
{
    private readonly IProjectionGateway<Projection.AccountDetails> _projectionGateway;

    public ListAccountsInteractor(IProjectionGateway<Projection.AccountDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.AccountDetails>> InteractAsync(Query.ListAccounts query, CancellationToken cancellationToken)
        => _projectionGateway.GetAllAsync(query.Limit, query.Offset, cancellationToken);
}