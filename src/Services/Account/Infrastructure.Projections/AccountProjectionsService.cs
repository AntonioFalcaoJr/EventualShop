using System.Linq.Expressions;
using Application.Abstractions.EventSourcing.Projections;
using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;
using Infrastructure.Projections.Abstractions.Pagination;

namespace Infrastructure.Projections;

public class AccountProjectionsService : IAccountProjectionsService
{
    private readonly IAccountProjectionsRepository _repository;

    public AccountProjectionsService(IAccountProjectionsRepository repository)
    {
        _repository = repository;
    }

    public Task<IPagedResult<AccountDetailsProjection>> GetAccountsAsync(int limit, int offset, CancellationToken cancellationToken)
        => _repository.GetAllAsync<AccountDetailsProjection>(
            paging: new Paging {Limit = limit, Offset = offset},
            predicate: projection => projection.IsDeleted == false,
            cancellationToken: cancellationToken);

    public Task<AccountDetailsProjection> GetAccountDetailsAsync(Guid accountId, CancellationToken cancellationToken)
        => _repository.GetAsync<AccountDetailsProjection, Guid>(accountId, cancellationToken);

    public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertAsync(projection, cancellationToken);

    public Task RemoveAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.DeleteAsync(filter, cancellationToken);
}