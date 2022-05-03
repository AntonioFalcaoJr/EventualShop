using Contracts.JsonConverters;
using Newtonsoft.Json;

namespace Contracts.DataTransferObjects;

public static class Dto
{
    public record Address(string City, string Country, int? Number, string State, string Street, string ZipCode);

    public record Customer(Guid? Id, Address ShippingAddress, Address BillingAddress);

    public record CreditCard([property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))] DateOnly Expiration, string HolderName, string Number, string SecurityNumber, Guid Id, decimal Amount) : IPaymentMethod;

    public record DebitCard([property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))] DateOnly Expiration, string HolderName, string Number, string SecurityNumber, Guid Id, decimal Amount) : IPaymentMethod;

    public record PayPal(string UserName, string Password, Guid Id, decimal Amount) : IPaymentMethod;

    public record CartItem(Guid? Id, Product Product, int Quantity);

    public record Product(Guid? Id, string Description, string Name, decimal UnitPrice, string PictureUrl, string Sku);

    public record Profile(string FirstName, string LastName, string Email, DateOnly Birthday);

    public interface IPaymentMethod
    {
        Guid Id { get; }
        decimal Amount { get; }
    }
}