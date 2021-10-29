using System;
using Messages.Abstractions.Commands;

namespace Messages.ShoppingCarts;

public static class Commands
{
    public record AddCartItem(Guid ProductId, string ProductName, decimal UnitPrice, Guid CartId, int Quantity) : Command;

    public record AddCreditCard(Guid CartId, DateOnly Expiration, string HolderName, string Number, string SecurityNumber) : Command;

    public record AddShippingAddress(Guid CartId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Command;

    public record ChangeBillingAddress(Guid CartId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Command;

    public record CreateCart(Guid CustomerId) : Command;

    public record CheckOutCart(Guid CartId) : Command;

    public record RemoveCartItem(Guid CartId, Guid ProductId) : Command;
}