using System;
using ECommerce.JsonConverters;
using Newtonsoft.Json;

namespace ECommerce.Contracts.Common;

public static class Models
{
    public record Address
    {
        public string City { get; init; }
        public string Country { get; init; }
        public int? Number { get; init; }
        public string State { get; init; }
        public string Street { get; init; }
        public string ZipCode { get; init; }
    }

    public record Customer
    {
        public Guid Id { get; init; }
        public Address ShippingAddress { get; init; }
        public Address BillingAddress { get; init; }
    }

    public record CreditCard : IPaymentMethod
    {
        public Guid Id { get; } = Guid.NewGuid();

        [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))]
        public DateOnly Expiration { get; init; }

        public string HolderName { get; init; }
        public string Number { get; init; }
        public string SecurityNumber { get; init; }
        public decimal Amount { get; init; }
    }

    public record DebitCard : IPaymentMethod
    {
        public Guid Id { get; } = Guid.NewGuid();

        [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))]
        public DateOnly Expiration { get; init; }

        public string HolderName { get; init; }
        public string Number { get; init; }
        public string SecurityNumber { get; init; }
        public decimal Amount { get; init; }
    }

    public record PayPal : IPaymentMethod
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string UserName { get; init; }
        public string Password { get; init; }
        public decimal Amount { get; init; }
    }

    public record ShoppingCartItem
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public string ProductName { get; init; }
        public decimal UnitPrice { get; init; }
        public int Quantity { get; init; }
        public string PictureUrl { get; init; }
    }

    public record Product
    {
        public string Sku { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int Quantity { get; init; }
    }

    public interface IPaymentMethod
    {
        Guid Id { get; }
        decimal Amount { get; }
    }
}