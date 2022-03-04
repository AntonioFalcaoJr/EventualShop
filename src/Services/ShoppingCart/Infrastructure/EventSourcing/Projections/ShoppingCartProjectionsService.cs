using System;
using System.Linq;
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

    public Task<ShoppingCartProjection> GetCartAsync(Guid cartId, CancellationToken cancellationToken)
        => _repository.FindAsync<ShoppingCartProjection>(cart => cart.Id == cartId, cancellationToken);

    public Task<ShoppingCartProjection> GetCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken)
        => _repository.FindAsync<ShoppingCartProjection>(cart => cart.CustomerId == customerId, cancellationToken);

    public Task<IPagedResult<ShoppingCartItemsProjection>> GetCartItemsAsync(Guid cartId, int limit, int offset, CancellationToken cancellationToken)
        => _repository.GetAllAsync<ShoppingCartItemsProjection>(
            paging: new Paging { Limit = limit, Offset = offset },
            predicate: cart => cart.Id == cartId,
            cancellationToken: cancellationToken);

    public Task<ShoppingCartItemsProjection> GetCartItemAsync(Guid cartId, Guid itemId, CancellationToken cancellationToken)
        => _repository.FindAsync<ShoppingCartItemsProjection>(
            predicate: cart => cart.Id == cartId && cart.Items.Any(item => item.Id == itemId),
            cancellationToken: cancellationToken);

    public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertAsync(projection, cancellationToken);

    public Task RemoveAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.DeleteAsync(filter, cancellationToken);
}