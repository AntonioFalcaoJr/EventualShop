using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.ShoppingCarts;

public static class DomainEvents
{
    public record CartCreated(Guid CartId, Guid CustomerId, string Status) : Event(CorrelationId: CustomerId);

    // TODO - ShoppingCartItem should be Product
    public record CartItemAdded(Guid CartId, Guid ItemId, Models.ShoppingCartItem Item) : Event(CorrelationId: CartId);

    public record CartItemIncreased(Guid CartId, Guid ItemId, decimal UnitPrice) : Event(CorrelationId: CartId);

    public record CartItemDecreased(Guid CartId, Guid ItemId, decimal UnitPrice) : Event(CorrelationId: CartId);

    public record CartItemRemoved(Guid CartId, Guid ItemId, decimal UnitPrice, int Quantity) : Event(CorrelationId: CartId);

    public record CartCheckedOut(Guid CartId) : Event(CorrelationId: CartId);

    public record ShippingAddressAdded(Guid CartId, Models.Address Address) : Event(CorrelationId: CartId);

    public record BillingAddressChanged(Guid CartId, Models.Address Address) : Event(CorrelationId: CartId);

    public record CreditCardAdded(Guid CartId, Models.CreditCard CreditCard) : Event(CorrelationId: CartId);

    public record PayPalAdded(Guid CartId, Models.PayPal PayPal) : Event(CorrelationId: CartId);

    public record CartDiscarded(Guid CartId) : Event(CorrelationId: CartId);
}