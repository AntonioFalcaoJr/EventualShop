using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;

namespace Application.EventSourcing.Projections
{
    public interface IAccountProjectionsService
    {
        Task<IPagedResult<AccountAuthenticationDetailsProjection>> GetAccountsDetailsWithPaginationAsync(Paging paging, Expression<Func<AccountAuthenticationDetailsProjection, bool>> predicate, CancellationToken cancellationToken);
        Task ProjectNewAccountDetailsAsync(AccountAuthenticationDetailsProjection accountAuthenticationDetails, CancellationToken cancellationToken);
        Task UpdateAccountDetailsAsync(AccountAuthenticationDetailsProjection accountAuthenticationDetails, CancellationToken cancellationToken);
        Task<AccountAuthenticationDetailsProjection> GetAccountDetailsAsync(Guid accountId, CancellationToken cancellationToken);
    }
}