using System.Linq.Expressions;
using Application.Abstractions.EventSourcing.Projections;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Projections;

namespace Infrastructure.EventSourcing.Projections;

public class AccountProjectionsService : IAccountProjectionsService
{
    private readonly IAccountProjectionsRepository _repository;

    public AccountProjectionsService(IAccountProjectionsRepository repository)
    {
        _repository = repository;
    }

    public Task<IPagedResult<AccountDetailsProjection>> GetAccountsDetailsWithPaginationAsync(Paging paging, Expression<Func<AccountDetailsProjection, bool>> predicate, CancellationToken cancellationToken)
        => _repository.GetAllAsync(paging, predicate, cancellationToken);

    public Task<AccountDetailsProjection> GetAccountDetailsAsync(Guid accountId, CancellationToken cancellationToken)
        => _repository.GetAsync<AccountDetailsProjection, Guid>(accountId, cancellationToken);

    public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertAsync(projection, cancellationToken);

    public Task RemoveAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.DeleteAsync(filter, cancellationToken);
}