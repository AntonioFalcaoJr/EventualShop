using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using Application.EventSourcing.Projections;

namespace Infrastructure.EventSourcing.Projections;

public class ShoppingCartProjectionsService : IShoppingCartProjectionsService
{
    private readonly IShoppingCartProjectionsRepository _repository;

    public ShoppingCartProjectionsService(IShoppingCartProjectionsRepository repository)
    {
        _repository = repository;
    }

    public Task<CartDetailsProjection> GetCartDetailsAsync(Guid customerId, CancellationToken cancellationToken)
        => _repository.FindAsync<CartDetailsProjection>(cart => cart.CustomerId == customerId, cancellationToken);

    public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertAsync(projection, cancellationToken);

    public Task RemoveAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.DeleteAsync(filter, cancellationToken);
}