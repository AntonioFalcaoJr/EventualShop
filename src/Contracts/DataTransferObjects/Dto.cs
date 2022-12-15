using Contracts.JsonConverters;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using CommunicationProtobuf = Contracts.Services.Communication.Protobuf;
using CatalogProtobuf = Contracts.Services.Catalog.Protobuf;

namespace Contracts.DataTransferObjects;

public static class Dto
{
    public record Address(string City, string Country, int? Number, string State, string Street, string ZipCode);

    public interface IPaymentOption { }

    public record CreditCard(
            [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))]
            [property: BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))]
            DateOnly Expiration, string Number, string HolderName, ushort SecurityNumber)
        : IPaymentOption;

    public record DebitCard(
            [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))]
            [property: BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))]
            DateOnly Expiration, string Number, string HolderName, ushort SecurityNumber)
        : IPaymentOption;

    public record PayPal(string UserName, string Password) : IPaymentOption;

    public record PaymentMethod(Guid Id, decimal Amount, IPaymentOption? Option);

    public interface INotificationOption { }

    public record struct NotificationMethod(Guid MethodId, INotificationOption? Option)
    {
        public static implicit operator CommunicationProtobuf.NotificationMethod(NotificationMethod method)
            => method.Option switch
            {
                Email email => new() { Email = email },
                Sms sms => new() { Sms = sms },
                PushMobile pushMobile => new() { PushMobile = pushMobile },
                PushWeb pushWeb => new() { PushWeb = pushWeb },
                _ => default
            };
    }

    public record struct Email(string Address, string Body) : INotificationOption
    {
        public static implicit operator CommunicationProtobuf.Email(Email email)
            => new() { Address = email.Address };
    }

    public record struct Sms(string Number, string Body) : INotificationOption
    {
        public static implicit operator CommunicationProtobuf.Sms(Sms sms)
            => new() { Number = sms.Number };
    }

    public record struct PushWeb(Guid UserId, string Body) : INotificationOption
    {
        public static implicit operator CommunicationProtobuf.PushWeb(PushWeb pushWeb)
            => new() { UserId = pushWeb.UserId.ToString() };
    }

    public record struct PushMobile(Guid DeviceId, string Body) : INotificationOption
    {
        public static implicit operator CommunicationProtobuf.PushMobile(PushMobile pushMobile)
            => new() { DeviceId = pushMobile.DeviceId.ToString() };
    }

    public record Product(string Description, string Name, string Brand, string Category, string Unit, string Sku)
    {
        public static implicit operator CatalogProtobuf.Product(Product product)
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

    public record InventoryItem(Guid Id, Guid InventoryId, Product Product, decimal Cost, int Quantity);

    public record CatalogItem(Guid Id, Guid CatalogId, Guid InventoryId, Product Product, decimal Cost, decimal Markup, int Quantity);

    public record CartItem(Guid Id, Product Product, ushort Quantity, decimal UnitPrice);

    public record OrderItem(Guid Id, Product Product, ushort Quantity, decimal UnitPrice)
    {
        public static implicit operator OrderItem(CartItem item)
            => new(Guid.NewGuid(), item.Product, item.Quantity, item.UnitPrice);
    }

    public record Profile(string FirstName, string LastName, string Email, DateOnly? Birthdate, string Gender);

    public record ShoppingCart(Guid Id, Guid CustomerId, string Status, Address BillingAddress, Address ShippingAddress, decimal Total, decimal TotalPayment, decimal AmountDue,
        IEnumerable<CartItem> Items, IEnumerable<PaymentMethod> PaymentMethods);
}