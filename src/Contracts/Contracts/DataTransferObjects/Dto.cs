using Contracts.JsonConverters;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Contracts.DataTransferObjects;

public static class Dto
{
    public record Address(string City, string Country, int? Number, string State, string Street, string ZipCode);

    public record Customer(Guid Id, Profile Profile);

    public record CreditCard([property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))] [property: BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))] DateOnly Expiration, string Number, string HolderName, string SecurityNumber)
        : IPaymentOption;

    public record DebitCard([property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))] [property: BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))] DateOnly Expiration, string Number, string HolderName, string SecurityNumber)
        : IPaymentOption;

    public record PayPal(string UserName, string Password) : IPaymentOption;

    public record PaymentMethod(Guid Id, decimal Amount, IPaymentOption Option);

    public record Product(string Description, string Name, decimal UnitPrice, string PictureUrl, string Sku);

    public abstract record Item(Guid Id, Product Product, int Quantity);

    public record CatalogItem(Guid Id, Product Product, int Quantity) : Item(Id, Product, Quantity);

    public record CartItem(Guid Id, Guid CatalogId, Guid InventoryId, Product Product, int Quantity) : Item(Id, Product, Quantity);

    public record InventoryItem(Guid Id, Product Product, int Quantity) : Item(Id, Product, Quantity);

    public record Profile(string FirstName, string LastName, string Email);

    public interface IPaymentOption { }
}