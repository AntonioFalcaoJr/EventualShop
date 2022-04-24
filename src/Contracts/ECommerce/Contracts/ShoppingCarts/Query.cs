using ECommerce.Abstractions.Messages;
using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.ShoppingCarts;

public static class Query
{
    public record GetShoppingCart(Guid CartId) : Message(CorrelationId: CartId), IQuery;

    public record GetCustomerShoppingCart(Guid CustomerId) : Message(CorrelationId: CustomerId), IQuery;

    public record GetShoppingCartItem(Guid CartId, Guid ItemId) : Message(CorrelationId: CartId), IQuery;

    public record GetShoppingCartItems(Guid CartId, int Limit, int Offset) : Message(CorrelationId: CartId), IQuery;

    public record GetShoppingCartPaymentMethods(Guid CartId, int Limit, int Offset) : Message(CorrelationId: CartId), IQuery;

    public record GetShoppingCartPaymentMethod(Guid CartId, Guid PaymentMethodId) : Message(CorrelationId: CartId), IQuery;
}