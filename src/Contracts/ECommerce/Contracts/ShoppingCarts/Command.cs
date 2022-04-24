using ECommerce.Abstractions;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.ShoppingCarts;

public static class Command
{
    public record AddCartItem(Guid CartId, Models.ShoppingCartItem Item) : Message(CorrelationId: CartId), ICommand;

    public record AddCreditCard(Guid CartId, Models.CreditCard CreditCard) : Message(CorrelationId: CartId), ICommand;

    public record AddPayPal(Guid CartId, Models.PayPal PayPal) : Message(CorrelationId: CartId), ICommand;

    public record AddShippingAddress(Guid CartId, Models.Address Address) : Message(CorrelationId: CartId), ICommand;

    public record ChangeBillingAddress(Guid CartId, Models.Address Address) : Message(CorrelationId: CartId), ICommand;

    public record CreateCart(Guid CustomerId) : Message(CorrelationId: CustomerId), ICommand;

    public record CheckOutCart(Guid CartId) : Message(CorrelationId: CartId), ICommand;

    public record RemoveCartItem(Guid CartId, Guid ItemId) : Message(CorrelationId: CartId), ICommand;

    public record IncreaseShoppingCartItem(Guid CartId, Guid ItemId) : Message(CorrelationId: CartId), ICommand;

    public record DecreaseShoppingCartItem(Guid CartId, Guid ItemId) : Message(CorrelationId: CartId), ICommand;

    public record DiscardCart(Guid CartId) : Message(CorrelationId: CartId), ICommand;
}