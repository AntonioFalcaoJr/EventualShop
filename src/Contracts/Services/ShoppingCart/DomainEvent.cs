using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class DomainEvent
{
    public record CartCreated(Guid CartId, Guid CustomerId, string? Status) : Message, IEvent;

    public record CartItemAdded(Guid CartId, Guid ItemId, Guid InventoryId, Guid CatalogId, Dto.Product Product, ushort Quantity, decimal UnitPrice) : Message, IEvent;

    public record CartItemIncreased(Guid CartId, Guid ItemId, ushort NewQuantity, decimal UnitPrice) : Message, IEvent;

    public record CartItemDecreased(Guid CartId, Guid ItemId, ushort NewQuantity, decimal UnitPrice) : Message, IEvent;

    public record CartItemRemoved(Guid CartId, Guid ItemId, decimal UnitPrice, int Quantity) : Message, IEvent;

    public record CartCheckedOut(Guid CartId) : Message, IEvent;

    public record ShippingAddressAdded(Guid CartId, Dto.Address Address) : Message, IEvent;

    public record BillingAddressAdded(Guid CartId, Dto.Address Address) : Message, IEvent;

    public record PaymentMethodAdded(Guid CartId, Guid MethodId, decimal Amount, Dto.IPaymentOption Option) : Message, IEvent;

    public record CartDiscarded(Guid CartId) : Message, IEvent;
}