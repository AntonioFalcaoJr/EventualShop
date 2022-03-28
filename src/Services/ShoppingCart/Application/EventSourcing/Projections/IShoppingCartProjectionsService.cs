using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;

namespace Application.EventSourcing.Projections;

public interface IShoppingCartProjectionsService : IProjectionsService
{
    Task<ShoppingCartProjection> GetShoppingCartAsync(Guid shoppingCartId, CancellationToken cancellationToken);
    Task<ShoppingCartProjection> GetShoppingCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
    Task<IPagedResult<ShoppingCartItemProjection>> GetShoppingCartItemsAsync(Guid shoppingCartId, int limit, int offset, CancellationToken cancellationToken);
    Task<ShoppingCartItemProjection> GetShoppingCartItemAsync(Guid shoppingCartId, Guid itemId, CancellationToken contextCancellationToken);
}