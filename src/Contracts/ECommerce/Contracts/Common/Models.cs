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
        [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))]
        public DateOnly Expiration { get; init; }
        public string HolderName { get; init; }
        public string Number { get; init; }
        public string SecurityNumber { get; init; }
        public Guid Id { get; init; }
        public decimal Amount { get; init; }
    }

    public record DebitCard : IPaymentMethod
    {
        [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))]
        public DateOnly Expiration { get; init; }
        public string HolderName { get; init; }
        public string Number { get; init; }
        public string SecurityNumber { get; init; }
        public Guid Id { get; init; }
        public decimal Amount { get; init; }
    }

    public record PayPal : IPaymentMethod
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public Guid Id { get; init; }
        public decimal Amount { get; init; }
    }

    public record ShoppingCartItem
    {
        public Product Product { get; init; }
        public int Quantity { get; init; }
    }

    public record Product
    {
        public Guid Id { get; init; }
        public string Sku { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal UnitPrice { get; init; }
        public string PictureUrl { get; init; }
    }

    public interface IPaymentMethod
    {
        Guid Id { get; }
        decimal Amount { get; }
    }

    public record Profile
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public DateOnly Birthday { get; init; }
    }
}