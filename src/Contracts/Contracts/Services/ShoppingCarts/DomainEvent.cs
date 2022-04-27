using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCarts;

public static class DomainEvent
{
    public record CartCreated(Guid CartId, Guid CustomerId, string Status) : Message(CorrelationId: CustomerId), IEvent;

    public record CartItemAdded(Guid CartId, Guid ItemId, Dto.Product Product, int Quantity) : Message(CorrelationId: CartId), IEvent;

    public record CartItemIncreased(Guid CartId, Guid ItemId, decimal UnitPrice) : Message(CorrelationId: CartId), IEvent;

    public record CartItemDecreased(Guid CartId, Guid ItemId, decimal UnitPrice) : Message(CorrelationId: CartId), IEvent;

    public record CartItemRemoved(Guid CartId, Guid ItemId, decimal UnitPrice, int Quantity) : Message(CorrelationId: CartId), IEvent;

    public record CartCheckedOut(Guid CartId) : Message(CorrelationId: CartId), IEvent;

    public record ShippingAddressAdded(Guid CartId, Dto.Address Address) : Message(CorrelationId: CartId), IEvent;

    public record BillingAddressChanged(Guid CartId, Dto.Address Address) : Message(CorrelationId: CartId), IEvent;

    public record CreditCardAdded(Guid CartId, Dto.CreditCard CreditCard) : Message(CorrelationId: CartId), IEvent;

    public record PayPalAdded(Guid CartId, Dto.PayPal PayPal) : Message(CorrelationId: CartId), IEvent;

    public record CartDiscarded(Guid CartId) : Message(CorrelationId: CartId), IEvent;
}