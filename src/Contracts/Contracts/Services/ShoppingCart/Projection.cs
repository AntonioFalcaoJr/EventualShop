using Contracts.Abstractions;
using Contracts.DataTransferObjects;
using Contracts.JsonConverters;
using MongoDB.Bson.Serialization.Attributes;

namespace Contracts.Services.ShoppingCart;

public static class Projection
{
    public record ShoppingCart(Guid Id, Customer Customer, string Status, decimal Total = default, bool IsDeleted = default) : IProjection;

    public record ShoppingCartItem(Guid CartId, Guid Id, Dto.Product Product, int Quantity, bool IsDeleted) : IProjection;

    public record Customer(Guid Id, Dto.Address ShippingAddress = default, Dto.Address BillingAddress = default);

    public interface IPaymentMethod : IProjection
    {
        decimal Amount { get; }
        Guid CartId { get; }
    }

    public record CreditCardPaymentMethod : IPaymentMethod
    {
        public Guid Id { get; init; }
        public Guid CartId { get; init; }
        public bool IsDeleted { get; init; }

        [BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))]
        public DateOnly Expiration { get; init; }

        public string HolderName { get; init; }
        public string Number { get; init; }
        public string SecurityNumber { get; init; }
        public decimal Amount { get; init; }
    }

    public record DebitCardPaymentMethod : IPaymentMethod
    {
        public Guid Id { get; init; }
        public Guid CartId { get; init; }
        public bool IsDeleted { get; init; }

        [BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))]
        public DateOnly Expiration { get; init; }

        public string HolderName { get; init; }
        public string Number { get; init; }
        public string SecurityNumber { get; init; }
        public decimal Amount { get; init; }
    }

    public record PayPalPaymentMethod : IPaymentMethod
    {
        public Guid Id { get; init; }
        public Guid CartId { get; init; }
        public bool IsDeleted { get; init; }
        public string Password { get; init; }
        public string UserName { get; init; }
        public decimal Amount { get; init; }
    }
}