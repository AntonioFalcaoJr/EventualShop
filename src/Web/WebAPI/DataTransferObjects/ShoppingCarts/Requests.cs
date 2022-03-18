using System;
using ECommerce.JsonConverters;
using Newtonsoft.Json;

namespace WebAPI.DataTransferObjects.ShoppingCarts;

public static class Requests
{
    public record CreateCart(Guid CustomerId);

    public record AddCartItem(Guid ProductId, string ProductName, decimal UnitPrice, int Quantity, string PictureUrl);

    public record PaymentWithPayPal(string UserName, string Password, decimal Amount);

    public record PaymentWithCreditCard(string HolderName, string Number, string SecurityNumber,
        [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))] DateOnly Expiration, decimal Amount);

    public record AddAddress(string City, string Country, int? Number, string State, string Street, string ZipCode);
}