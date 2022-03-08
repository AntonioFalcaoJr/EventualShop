using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;

namespace Application.EventSourcing.Projections;

public interface IShoppingCartProjectionsService : IProjectionsService
{
    Task<ShoppingCartProjection> GetShoppingCartAsync(Guid cartId, CancellationToken cancellationToken);
    Task<ShoppingCartProjection> GetShoppingCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
    Task<IPagedResult<ShoppingCartItemProjection>> GetShoppingCartItemsAsync(Guid cartId, int limit, int offset, CancellationToken cancellationToken);
    Task<ShoppingCartItemProjection> GetShoppingCartItemAsync(Guid cartId, Guid itemId, CancellationToken contextCancellationToken);
}