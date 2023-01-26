using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class DomainEvent
{
    public record CartCreated(Guid CartId, Guid CustomerId, Dto.Money Total, string Status, long Version) : Message, IVersionedEvent;

    public record CartItemAdded(Guid CartId, Guid ItemId, Guid InventoryId, Dto.Product Product, ushort Quantity, Dto.Money UnitPrice, Dto.Money NewCartTotal, long Version) : Message, IVersionedEvent;

    public record CartItemIncreased(Guid CartId, Guid ItemId, ushort NewQuantity, Dto.Money UnitPrice, Dto.Money NewCartTotal, long Version) : Message, IVersionedEvent;

    public record CartItemDecreased(Guid CartId, Guid ItemId, ushort NewQuantity, Dto.Money UnitPrice, Dto.Money NewCartTotal, long Version) : Message, IVersionedEvent;

    public record CartItemRemoved(Guid CartId, Guid ItemId, Dto.Money UnitPrice, int Quantity, Dto.Money NewCartTotal, long Version) : Message, IVersionedEvent;

    public record CartCheckedOut(Guid CartId, string Status, long Version) : Message, IVersionedEvent;

    public record ShippingAddressAdded(Guid CartId, Dto.Address Address, long Version) : Message, IVersionedEvent;

    public record BillingAddressAdded(Guid CartId, Dto.Address Address, long Version) : Message, IVersionedEvent;

    public record CartDiscarded(Guid CartId, string Status, long Version) : Message, IVersionedEvent;

    public record CreditCardAdded(Guid CartId, Guid MethodId, Dto.Money Amount, Dto.CreditCard CreditCard, long Version) : Message, IVersionedEvent;

    public record DebitCardAdded(Guid CartId, Guid MethodId, Dto.Money Amount, Dto.DebitCard DebitCard, long Version) : Message, IVersionedEvent;

    public record PayPalAdded(Guid CartId, Guid MethodId, Dto.Money Amount, Dto.PayPal PayPal, long Version) : Message, IVersionedEvent;
}