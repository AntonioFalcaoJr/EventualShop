using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;

namespace Application.EventSourcing.Projections
{
    public interface IShoppingCartProjectionsService
    {
        Task<IPagedResult<AccountDetailsProjection>> GetAccountsDetailsWithPaginationAsync(Paging paging, Expression<Func<AccountDetailsProjection, bool>> predicate, CancellationToken cancellationToken);
        Task ProjectNewAccountDetailsAsync(AccountDetailsProjection accountDetails, CancellationToken cancellationToken);
        Task<AccountDetailsProjection> GetAccountDetailsAsync(Guid accountId, CancellationToken cancellationToken);
    }
}