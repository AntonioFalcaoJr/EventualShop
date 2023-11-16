using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Boundaries.Account;

namespace Application.UseCases.Queries;

public class ListAccountsInteractor(IProjectionGateway<Projection.AccountDetails> projectionGateway)
    : IPagedInteractor<Query.ListAccountsDetails, Projection.AccountDetails>
{
    public ValueTask<IPagedResult<Projection.AccountDetails>> InteractAsync(Query.ListAccountsDetails query, CancellationToken cancellationToken)
        => projectionGateway.ListAsync(query.Paging, cancellationToken);
}