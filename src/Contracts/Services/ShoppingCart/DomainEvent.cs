using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class DomainEvent
{
    public record CartCreated(string CartId, string CustomerId, Dto.Money Total, string Status, long Version) : Message, IDomainEvent;

    public record CartItemAdded(string CartId, string ItemId, Guid InventoryId, Dto.Product Product, ushort Quantity, Dto.Money UnitPrice, Dto.Money NewCartTotal, long Version) : Message, IDomainEvent;

    public record CartItemIncreased(string CartId, string ItemId, int NewQuantity, Dto.Money UnitPrice, Dto.Money NewCartTotal, long Version) : Message, IDomainEvent;

    public record CartItemDecreased(string CartId, string ItemId, int NewQuantity, Dto.Money UnitPrice, Dto.Money NewCartTotal, long Version) : Message, IDomainEvent;

    public record CartItemRemoved(string CartId, string ItemId, Dto.Money UnitPrice, int Quantity, Dto.Money NewCartTotal, long Version) : Message, IDomainEvent;

    public record CartCheckedOut(string CartId, string Status, long Version) : Message, IDomainEvent;

    public record ShippingAddressAdded(string CartId, Dto.Address Address, long Version) : Message, IDomainEvent;

    public record BillingAddressAdded(string CartId, Dto.Address Address, long Version) : Message, IDomainEvent;

    public record CartDiscarded(string CartId, string Status, long Version) : Message, IDomainEvent;

    public record CreditCardAdded(string CartId, Guid MethodId, Dto.Money Amount, Dto.CreditCard CreditCard, long Version) : Message, IDomainEvent;

    public record DebitCardAdded(string CartId, Guid MethodId, Dto.Money Amount, Dto.DebitCard DebitCard, long Version) : Message, IDomainEvent;

    public record PayPalAdded(string CartId, Guid MethodId, Dto.Money Amount, Dto.PayPal PayPal, long Version) : Message, IDomainEvent;
}