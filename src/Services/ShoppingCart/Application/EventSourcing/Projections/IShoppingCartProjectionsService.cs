using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;

namespace Application.EventSourcing.Projections
{
    public interface IShoppingCartProjectionsService
    {
        Task<IPagedResult<CartDetailsProjection>> GetAccountsDetailsWithPaginationAsync(Paging paging, Expression<Func<CartDetailsProjection, bool>> predicate, CancellationToken cancellationToken);
        Task ProjectCartDetailsAsync(CartDetailsProjection cartDetails, CancellationToken cancellationToken);
        Task<CartDetailsProjection> GetAccountDetailsAsync(Guid accountId, CancellationToken cancellationToken);
    }
}