using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class DomainEvent
{
    public record CartCreated(Guid Id, Guid CustomerId, string Status) : Message(CorrelationId: CustomerId), IEvent;

    public record CartItemAdded(Guid Id, Guid ItemId, Guid InventoryId, Guid CatalogId, Dto.Product Product, ushort Quantity, decimal UnitPrice) : Message, IEvent;

    public record CartItemIncreased(Guid Id, Guid ItemId, ushort NewQuantity, decimal UnitPrice) : Message, IEvent;

    public record CartItemDecreased(Guid Id, Guid ItemId, ushort NewQuantity, decimal UnitPrice) : Message, IEvent;

    public record CartItemRemoved(Guid Id, Guid ItemId, decimal UnitPrice, int Quantity) : Message, IEvent;

    public record CartCheckedOut(Guid Id) : Message, IEvent;

    public record ShippingAddressAdded(Guid Id, Dto.Address Address) : Message, IEvent;

    public record BillingAddressAdded(Guid Id, Dto.Address Address) : Message, IEvent;

    public record PaymentMethodAdded(Guid Id, Guid MethodId, decimal Amount, Dto.IPaymentOption Option) : Message, IEvent;

    public record CartDiscarded(Guid Id) : Message, IEvent;
}