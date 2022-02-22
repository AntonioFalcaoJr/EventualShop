using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;

namespace Application.EventSourcing.Projections;

public interface IShoppingCartProjectionsService : IProjectionsService
{
    Task<CartProjection> GetCartAsync(Guid cartId, CancellationToken cancellationToken);
    Task<CartProjection> GetCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
    Task<IPagedResult<CartItemsProjection>> GetCartItemsAsync(Guid cartId, int limit, int offset, CancellationToken cancellationToken);
    Task<CartItemsProjection> GetCartItemAsync(Guid cartId, Guid itemId, CancellationToken contextCancellationToken);
}