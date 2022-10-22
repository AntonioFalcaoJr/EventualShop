using Contracts.Abstractions.Messages;

namespace Contracts.Services.ShoppingCart;

public static class Query
{
    public record GetShoppingCart(Guid CartId) : Message, IQuery;

    public record GetCustomerShoppingCart(Guid CustomerId) : Message, IQuery;

    public record GetShoppingCartItem(Guid CartId, Guid ItemId) : Message, IQuery;

    public record GetShoppingCartItems(Guid CartId, ushort Limit, ushort Offset) : Message, IQuery;

    public record GetCartPaymentMethods(Guid CartId, ushort Limit, ushort Offset) : Message, IQuery;

    public record GetCartPaymentMethod(Guid CartId, Guid PaymentMethodId) : Message, IQuery;
}