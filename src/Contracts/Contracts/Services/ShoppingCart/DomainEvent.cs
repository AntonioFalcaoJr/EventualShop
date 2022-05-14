using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class DomainEvent
{
    public record CartCreated(Guid CartId, Guid CustomerId, string Status) : Message(CorrelationId: CustomerId), IEvent;

    public record CartItemAdded(Guid Id, Guid CartId, Dto.CatalogItem CatalogItem) : Message(CorrelationId: CartId), IEvent;

    public record CartItemIncreased(Guid CartId, Guid ItemId, decimal UnitPrice) : Message(CorrelationId: CartId), IEvent;

    public record CartItemDecreased(Guid CartId, Guid ItemId, decimal UnitPrice) : Message(CorrelationId: CartId), IEvent;

    public record CartItemRemoved(Guid CartId, Guid ItemId, decimal UnitPrice, int Quantity) : Message(CorrelationId: CartId), IEvent;

    public record CartItemConfirmed(Guid CartId, Guid ItemId, Guid CatalogId, int Quantity) : Message(CorrelationId: CartId), IEvent;

    public record CartCheckedOut(Guid CartId) : Message(CorrelationId: CartId), IEvent;

    public record ShippingAddressAdded(Guid CartId, Dto.Address Address) : Message(CorrelationId: CartId), IEvent;

    public record BillingAddressChanged(Guid CartId, Dto.Address Address) : Message(CorrelationId: CartId), IEvent;

    public record PaymentMethodAdded(Guid CartId, Guid MethodId, decimal Amount, Dto.IPaymentOption Option) : Message(CorrelationId: CartId), IEvent;

    public record CartDiscarded(Guid CartId) : Message(CorrelationId: CartId), IEvent;
}