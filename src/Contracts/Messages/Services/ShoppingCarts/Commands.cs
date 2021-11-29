using System;
using Messages.Abstractions.Commands;

namespace Messages.Services.ShoppingCarts;

public static class Commands
{
    public record AddCartItem(Guid CartId, Models.Item Item) : Command;

    public record AddCreditCard(Guid CartId, Models.CreditCard CreditCard) : Command;

    public record AddShippingAddress(Guid CartId, Models.Address Address) : Command;

    public record ChangeBillingAddress(Guid CartId, Models.Address Address) : Command;

    public record CreateCart(Guid CustomerId) : Command;

    public record CheckOutCart(Guid CartId) : Command;

    public record RemoveCartItem(Guid CartId, Guid ProductId) : Command;

    public record UpdateCartItemQuantity(Guid CartId, Guid ProductId, int Quantity) : Command;

    public record DiscardCart(Guid CartId) : Command;
}