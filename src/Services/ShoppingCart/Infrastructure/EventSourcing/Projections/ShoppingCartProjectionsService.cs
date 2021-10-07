using System;
using System.Threading;
using System.Threading.Tasks;
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

        public Task ProjectCartDetailsAsync(CartDetailsProjection cartDetails, CancellationToken cancellationToken)
            => _repository.SaveAsync(cartDetails, cancellationToken);
        
        public Task UpdateCartDetailsAsync(CartDetailsProjection cartDetails, CancellationToken cancellationToken)
            => _repository.UpsertAsync(cartDetails, cancellationToken);

        public Task<CartDetailsProjection> GetCartDetailsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
            => _repository.FindAsync<CartDetailsProjection>(projection => projection.UserId == userId, cancellationToken);
    }
}