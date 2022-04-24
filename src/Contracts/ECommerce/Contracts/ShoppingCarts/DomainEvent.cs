using ECommerce.Abstractions;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.ShoppingCarts;

public static class DomainEvent
{
    public record CartCreated(Guid CartId, Guid CustomerId, string Status) : Message(CorrelationId: CustomerId), IEvent;

    // TODO - ShoppingCartItem should be Product
    public record CartItemAdded(Guid CartId, Guid ItemId, Models.ShoppingCartItem Item) : Message(CorrelationId: CartId), IEvent;

    public record CartItemIncreased(Guid CartId, Guid ItemId, decimal UnitPrice) : Message(CorrelationId: CartId), IEvent;

    public record CartItemDecreased(Guid CartId, Guid ItemId, decimal UnitPrice) : Message(CorrelationId: CartId), IEvent;

    public record CartItemRemoved(Guid CartId, Guid ItemId, decimal UnitPrice, int Quantity) : Message(CorrelationId: CartId), IEvent;

    public record CartCheckedOut(Guid CartId) : Message(CorrelationId: CartId), IEvent;

    public record ShippingAddressAdded(Guid CartId, Models.Address Address) : Message(CorrelationId: CartId), IEvent;

    public record BillingAddressChanged(Guid CartId, Models.Address Address) : Message(CorrelationId: CartId), IEvent;

    public record CreditCardAdded(Guid CartId, Models.CreditCard CreditCard) : Message(CorrelationId: CartId), IEvent;

    public record PayPalAdded(Guid CartId, Models.PayPal PayPal) : Message(CorrelationId: CartId), IEvent;

    public record CartDiscarded(Guid CartId) : Message(CorrelationId: CartId), IEvent;
}