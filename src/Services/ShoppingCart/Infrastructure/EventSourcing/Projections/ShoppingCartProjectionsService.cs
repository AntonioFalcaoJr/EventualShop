using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;
using Infrastructure.Abstractions.EventSourcing.Projections.Pagination;

namespace Infrastructure.EventSourcing.Projections;

public class ShoppingCartProjectionsService : IShoppingCartProjectionsService
{
    private readonly IShoppingCartProjectionsRepository _repository;

    public ShoppingCartProjectionsService(IShoppingCartProjectionsRepository repository)
    {
        _repository = repository;
    }

    public Task<ShoppingCartProjection> GetShoppingCartAsync(Guid shoppingCartId, CancellationToken cancellationToken)
        => _repository.FindAsync<ShoppingCartProjection>(shoppingCart => shoppingCart.Id == shoppingCartId, cancellationToken);

    public Task<ShoppingCartProjection> GetShoppingCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken)
        => _repository.FindAsync<ShoppingCartProjection>(shoppingCart => shoppingCart.Customer.Id == customerId, cancellationToken);

    public Task<IPagedResult<ShoppingCartItemProjection>> GetShoppingCartItemsAsync(Guid shoppingCartId, int limit, int offset, CancellationToken cancellationToken)
        => _repository.GetAllAsync<ShoppingCartItemProjection>(
            paging: new Paging { Limit = limit, Offset = offset },
            predicate: item => item.ShoppingCartId == shoppingCartId,
            cancellationToken: cancellationToken);

    public Task<ShoppingCartItemProjection> GetShoppingCartItemAsync(Guid shoppingCartId, Guid itemId, CancellationToken cancellationToken)
        => _repository.FindAsync<ShoppingCartItemProjection>(
            predicate: item => item.ShoppingCartId == shoppingCartId && item.Id == itemId,
            cancellationToken: cancellationToken);

    public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertAsync(projection, cancellationToken);

    public Task ProjectManyAsync<TProjection>(IEnumerable<TProjection> projections, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertManyAsync(projections, cancellationToken);

    public Task RemoveAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.DeleteAsync(filter, cancellationToken);
}