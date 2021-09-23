using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Projections;

namespace Infrastructure.EventSourcing.Projections
{
    public class AccountProjectionsService : IAccountProjectionsService
    {
        private readonly IAccountProjectionsRepository _repository;

        public AccountProjectionsService(IAccountProjectionsRepository repository)
        {
            _repository = repository;
        }

        public Task<IPagedResult<AccountDetailsProjection>> GetAccountsDetailsWithPaginationAsync(Paging paging, Expression<Func<AccountDetailsProjection, bool>> predicate, CancellationToken cancellationToken)
            => _repository.GetAllAsync(paging, predicate, cancellationToken);

        public Task ProjectNewAccountDetailsAsync(AccountDetailsProjection accountDetails, CancellationToken cancellationToken)
            => _repository.SaveAsync(accountDetails, cancellationToken);

        public Task<AccountDetailsProjection> GetAccountDetailsAsync(Guid accountId, CancellationToken cancellationToken)
            => _repository.GetAsync<AccountDetailsProjection, Guid>(accountId, cancellationToken);
    }
}