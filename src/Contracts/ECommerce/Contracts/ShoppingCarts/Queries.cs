using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.ShoppingCarts;

public static class Queries
{
    public record GetShoppingCart(Guid CartId) : Query(CorrelationId: CartId);

    public record GetCustomerShoppingCart(Guid CustomerId) : Query(CorrelationId: CustomerId);

    public record GetShoppingCartItem(Guid CartId, Guid ItemId) : Query(CorrelationId: CartId);

    public record GetShoppingCartItems(Guid CartId, int Limit, int Offset) : QueryPaging(Limit, Offset, CorrelationId: CartId);
}