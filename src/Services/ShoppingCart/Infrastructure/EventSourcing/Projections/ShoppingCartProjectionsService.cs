using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
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

        public Task ProjectNewAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
            where TProjection : IProjection
            => _repository.SaveAsync(projection, cancellationToken);

        public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
            where TProjection : IProjection
            => _repository.UpsertAsync(projection, cancellationToken);

        public Task<CartDetailsProjection> GetCartDetailsAsync(Guid userId, CancellationToken cancellationToken)
            => _repository.FindAsync<CartDetailsProjection>(cart => cart.UserId == userId, cancellationToken);

        public Task DiscardCartAsync(Guid userId, CancellationToken cancellationToken)
            => _repository.DeleteAsync<CartDetailsProjection>(cart => cart.UserId == userId, cancellationToken);
    }
}