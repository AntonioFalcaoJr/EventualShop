using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Projections;

namespace Infrastructure.EventSourcing.Accounts.Projections
{
    public class AccountProjectionsService : IAccountProjectionsService
    {
        private readonly IAccountProjectionsRepository _repository;

        public AccountProjectionsService(IAccountProjectionsRepository repository)
        {
            _repository = repository;
        }

        public Task<IPagedResult<AccountAuthenticationDetailsProjection>> GetAccountsDetailsWithPaginationAsync(Paging paging, Expression<Func<AccountAuthenticationDetailsProjection, bool>> predicate, CancellationToken cancellationToken)
            => _repository.GetAllAsync(paging, predicate, cancellationToken);

        public Task ProjectNewAccountDetailsAsync(AccountAuthenticationDetailsProjection accountAuthenticationDetails, CancellationToken cancellationToken)
            => _repository.SaveAsync(accountAuthenticationDetails, cancellationToken);
        
        public Task UpdateAccountDetailsAsync(AccountAuthenticationDetailsProjection accountAuthenticationDetails, CancellationToken cancellationToken)
            => _repository.UpdateAsync(accountAuthenticationDetails, cancellationToken);

        public Task<AccountAuthenticationDetailsProjection> GetAccountDetailsAsync(Guid accountId, CancellationToken cancellationToken)
            => _repository.GetAsync<AccountAuthenticationDetailsProjection, Guid>(accountId, cancellationToken);
    }
}