using System;
using ECommerce.Abstractions.Messages.Commands;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.ShoppingCart;

public static class Commands
{
    public record AddCartItem(Guid CartId, Models.Item Item) : Command(CorrelationId: CartId);

    public record AddCreditCard(Guid CartId, Models.CreditCard CreditCard) : Command(CorrelationId: CartId);

    public record AddPayPal(Guid CartId, Models.PayPal PayPal) : Command(CorrelationId: CartId);

    public record AddShippingAddress(Guid CartId, Models.Address Address) : Command(CorrelationId: CartId);

    public record ChangeBillingAddress(Guid CartId, Models.Address Address) : Command(CorrelationId: CartId);

    public record CreateCart(Guid CustomerId) : Command(CorrelationId: CustomerId);

    public record CheckOutCart(Guid CartId) : Command(CorrelationId: CartId);

    public record RemoveCartItem(Guid CartId, Guid ItemId) : Command(CorrelationId: CartId);

    public record IncreaseCartItemQuantity(Guid CartId, Guid ItemId) : Command(CorrelationId: CartId);

    public record DecreaseCartItemQuantity(Guid CartId, Guid ItemId) : Command(CorrelationId: CartId);

    public record DiscardCart(Guid CartId) : Command(CorrelationId: CartId);
}