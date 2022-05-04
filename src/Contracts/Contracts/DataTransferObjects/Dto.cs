using Contracts.JsonConverters;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Contracts.DataTransferObjects;

public static class Dto
{
    public record Address(string City, string Country, int? Number, string State, string Street, string ZipCode);

    public record Customer(Guid? Id, Address ShippingAddress, Address BillingAddress);

    public record CreditCard(Guid Id, decimal Amount, [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))] [property: BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))] DateOnly Expiration, string Number, string HolderName, string SecurityNumber) : IPaymentMethod;

    public record DebitCard(Guid Id, decimal Amount, [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))] [property: BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))] DateOnly Expiration, string Number, string HolderName, string SecurityNumber) : IPaymentMethod;

    public record PayPal(Guid Id, decimal Amount, string UserName, string Password) : IPaymentMethod;

    public record CartItem(Guid? Id, Product Product, int Quantity);

    public record Product(Guid? Id, string Description, string Name, decimal UnitPrice, string PictureUrl, string Sku);

    public record Profile(string FirstName, string LastName, string Email, DateOnly Birthday, Address ResidenceAddress, Address ProfessionalAddress);

    public interface IPaymentMethod
    {
        Guid Id { get; }
        decimal Amount { get; }
    }
}