using Contracts.JsonConverters;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using CommunicationProtobuf = Contracts.Services.Communication.Protobuf;
using ShoppingCartProtobuf = Contracts.Services.ShoppingCart.Protobuf;
using CatalogProtobuf = Contracts.Services.Catalog.Protobuf;

namespace Contracts.DataTransferObjects;

public static class Dto
{
    public record Money(string Amount, string Currency)
    {
        public static implicit operator Abstractions.Protobuf.Money(Money money)
            => new()
            {
                Amount = money.Amount,
                Currency = money.Currency
            };
    }

    public record Address(string Street, string City, string State, string ZipCode, string Country, int? Number, string? Complement)
    {
        public static implicit operator Abstractions.Protobuf.Address(Address address)
            => new()
            {
                Street = address.Street,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Country = address.Country,
                Number = address.Number,
                Complement = address.Complement
            };
    }

    public interface IPaymentOption { }

    public record CreditCard(
        [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))]
        [property: BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))]
        DateOnly ExpirationDate, string Number, string HolderName, ushort SecurityCode) : IPaymentOption
    {
        public static implicit operator Abstractions.Protobuf.CreditCard(CreditCard creditCard)
            => new()
            {
                ExpirationDate = creditCard.ExpirationDate.ToShortDateString(),
                Number = creditCard.Number,
                HolderName = creditCard.HolderName,
                SecurityCode = creditCard.SecurityCode
            };
    }

    public record DebitCard(
        [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))]
        [property: BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))]
        DateOnly ExpirationDate, string Number, string HolderName, ushort SecurityCode) : IPaymentOption
    {
        public static implicit operator Abstractions.Protobuf.DebitCard(DebitCard debitCard)
            => new()
            {
                ExpirationDate = debitCard.ExpirationDate.ToShortDateString(),
                Number = debitCard.Number,
                HolderName = debitCard.HolderName,
                SecurityCode = debitCard.SecurityCode
            };
    }

    public record PayPal(string Email, string Password) : IPaymentOption
    {
        public static implicit operator Abstractions.Protobuf.PayPal(PayPal payPal)
            => new() { Email = payPal.Email };
    }

    public record PaymentMethod(Guid Id, Money Amount, IPaymentOption Option);

    public interface INotificationOption { }

    public record NotificationMethod(Guid Id, INotificationOption Option);

    public record Email(string Address, string Subject, string Body) : INotificationOption
    {
        public static implicit operator Abstractions.Protobuf.Email(Email email)
            => new() { Address = email.Address };
    }

    public record Sms(string Number, string Body) : INotificationOption
    {
        public static implicit operator Abstractions.Protobuf.Sms(Sms sms)
            => new() { Number = sms.Number };
    }

    public record PushWeb(Guid UserId, string Body) : INotificationOption
    {
        public static implicit operator Abstractions.Protobuf.PushWeb(PushWeb pushWeb)
            => new() { UserId = pushWeb.UserId.ToString() };
    }

    public record PushMobile(Guid DeviceId, string Body) : INotificationOption
    {
        public static implicit operator Abstractions.Protobuf.PushMobile(PushMobile pushMobile)
            => new() { DeviceId = pushMobile.DeviceId.ToString() };
    }

    public record Product(string Description, string Name, string Brand, string Category, string Unit, string Sku)
    {
        public static implicit operator Abstractions.Protobuf.Product(Product product)
            => new()
            {
                Description = product.Description,
                Name = product.Name,
                Brand = product.Brand,
                Category = product.Category,
                Unit = product.Unit,
                Sku = product.Sku
            };
    }

    public record InventoryItem(Guid Id, Guid InventoryId, Product Product, string Cost, int Quantity);

    public record CatalogItem(Guid Id, Guid CatalogId, Guid InventoryId, Product Product, string Cost, decimal Markup, int Quantity);

    public record CartItem(Guid Id, Product Product, ushort Quantity, Money UnitPrice);

    public record OrderItem(Guid Id, Product Product, ushort Quantity, Money UnitPrice)
    {
        public static implicit operator OrderItem(CartItem item)
            => new(Guid.NewGuid(), item.Product, item.Quantity, item.UnitPrice);
    }

    public record Profile(string FirstName, string LastName, string Email, DateOnly? Birthdate, string Gender);

    public record ShoppingCart(Guid Id, Guid CustomerId, string Status, Address BillingAddress, Address ShippingAddress, Money Total, Money TotalPayment, Money AmountDue,
        IEnumerable<CartItem> Items, IEnumerable<PaymentMethod> PaymentMethods);
}