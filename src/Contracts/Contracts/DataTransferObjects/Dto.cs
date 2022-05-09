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

    public record CartItem(Guid Id, Product Product, int Quantity);

    public record Product(Guid? Id, string Description, string Name, decimal UnitPrice, string PictureUrl, string Sku);

    public record Profile(string FirstName, string LastName, string Email);

    public interface IPaymentOption { }
}