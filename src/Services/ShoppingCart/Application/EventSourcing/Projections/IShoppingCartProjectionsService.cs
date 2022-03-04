using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;

namespace Application.EventSourcing.Projections;

public interface IShoppingCartProjectionsService : IProjectionsService
{
    Task<ShoppingCartProjection> GetCartAsync(Guid cartId, CancellationToken cancellationToken);
    Task<ShoppingCartProjection> GetCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
    Task<IPagedResult<ShoppingCartItemProjection>> GetCartItemsAsync(Guid cartId, int limit, int offset, CancellationToken cancellationToken);
    Task<ShoppingCartItemProjection> GetCartItemAsync(Guid cartId, Guid itemId, CancellationToken contextCancellationToken);
}