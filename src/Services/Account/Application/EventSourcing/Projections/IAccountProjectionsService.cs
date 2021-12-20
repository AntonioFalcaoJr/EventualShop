using System.Linq.Expressions;
using Application.Abstractions.EventSourcing.Projections;
using Application.Abstractions.EventSourcing.Projections.Pagination;

namespace Application.EventSourcing.Projections;

public interface IAccountProjectionsService : IProjectionsService
{
    Task<IPagedResult<AccountDetailsProjection>> GetAccountsDetailsWithPaginationAsync(Paging paging, Expression<Func<AccountDetailsProjection, bool>> predicate, CancellationToken cancellationToken);
    Task<AccountDetailsProjection> GetAccountDetailsAsync(Guid accountId, CancellationToken cancellationToken);
}