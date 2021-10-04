using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Projections;

namespace Infrastructure.EventSourcing.Projections
{
    public class ShoppingCartProjectionsService : IShoppingCartProjectionsService
    {
        private readonly IShoppingCartProjectionsRepository _repository;

        public ShoppingCartProjectionsService(IShoppingCartProjectionsRepository repository)
        {
            _repository = repository;
        }

        public Task<IPagedResult<CartDetailsProjection>> GetAccountsDetailsWithPaginationAsync(Paging paging, Expression<Func<CartDetailsProjection, bool>> predicate, CancellationToken cancellationToken)
            => _repository.GetAllAsync(paging, predicate, cancellationToken);

        public Task ProjectCartDetailsAsync(CartDetailsProjection cartDetails, CancellationToken cancellationToken)
            => _repository.SaveAsync(cartDetails, cancellationToken);

        public Task<CartDetailsProjection> GetAccountDetailsAsync(Guid accountId, CancellationToken cancellationToken)
            => _repository.GetAsync<CartDetailsProjection, Guid>(accountId, cancellationToken);
    }
}