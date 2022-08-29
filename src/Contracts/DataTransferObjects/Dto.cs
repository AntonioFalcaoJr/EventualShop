using Contracts.JsonConverters;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Contracts.DataTransferObjects;

public static class Dto
{
    public record Address(string City, string Country, int? Number, string State, string Street, string ZipCode);

    public record Customer(Guid Id, Profile Profile);

    public record CreditCard(
            [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))]
            [property: BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))]
            DateOnly Expiration, string Number, string HolderName, string SecurityNumber)
        : IPaymentOption;

    public record DebitCard(
            [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))]
            [property: BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))]
            DateOnly Expiration, string Number, string HolderName, string SecurityNumber)
        : IPaymentOption;

    public record PayPal(string UserName, string Password) : IPaymentOption;

    public record PaymentMethod(Guid Id, decimal Amount, IPaymentOption Option);

    public interface IPaymentOption { }

    public record Product(string Description, string Name, string PictureUrl, string Brand, string Category, string Unit);

    public record InventoryItem(Guid Id, Guid InventoryId, Product Product, string Sku, decimal Cost, int Quantity);

    public record CatalogItem(Guid Id, Guid CatalogId, Guid InventoryId, Product Product, string Sku, decimal Cost, decimal Markup, int Quantity);

    public record CartItem(Guid Id, Guid CatalogId, Product Product, int Quantity, string Sku, decimal UnitPrice);

    public record OrderItem(Guid Id, Guid OrderId, Guid CartId, Guid CatalogId, Guid InventoryId, Product Product, string Sku, int Quantity, decimal UnitPrice);

    public record Profile(string FirstName, string LastName, string Email, DateOnly Birthdate, string Gender);
}