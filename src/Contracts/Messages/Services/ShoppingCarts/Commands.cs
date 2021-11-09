using System;
using Messages.Abstractions.Commands;
using Messages.JsonConverters;
using Newtonsoft.Json;

namespace Messages.Services.ShoppingCarts;

public static class Commands
{
    public record AddCartItem(Guid CartId, Models.Product Product, int Quantity) : Command;

    public record AddCreditCard(Guid CartId, [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))] DateOnly Expiration, string HolderName, string Number, string SecurityNumber) : Command;

    public record AddShippingAddress(Guid CartId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Command;

    public record ChangeBillingAddress(Guid CartId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Command;

    public record CreateCart(Guid CustomerId) : Command;

    public record CheckOutCart(Guid CartId) : Command;

    public record RemoveCartItem(Guid CartId, Guid ProductId) : Command;

    public record IncreaseCartItemQuantity(Guid CartId, Guid ProductId, int Quantity) : Command;

    public record DecreaseCartItemQuantity(Guid CartId, Guid ProductId, int Quantity) : Command;
}