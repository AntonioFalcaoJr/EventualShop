using System;
using ECommerce.Contracts.Common;
using ECommerce.JsonConverters;
using Newtonsoft.Json;

namespace WebAPI.DataTransferObjects.ShoppingCarts;

public static class Requests
{
    public record CreateCart(Guid CustomerId);

    public record AddCartItem(Guid ProductId, string ProductName, decimal UnitPrice, int Quantity, string PictureUrl);
    
    public record PaymentWithPayPal : Models.IPaymentMethod
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string UserName { get; init; }
        public string Password { get; init; }
        public decimal Amount { get; init; }
    }
    
    public record PaymentWithCreditCard : Models.IPaymentMethod
    {
        public Guid Id { get; } = Guid.NewGuid();

        [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))]
        public DateOnly Expiration { get; init; }

        public string HolderName { get; init; }
        public string Number { get; init; }
        public string SecurityNumber { get; init; }
        public decimal Amount { get; init; }
    }
    
    public record AddAddress
    {
        public string City { get; init; }
        public string Country { get; init; }
        public int? Number { get; init; }
        public string State { get; init; }
        public string Street { get; init; }
        public string ZipCode { get; init; }
    }
}