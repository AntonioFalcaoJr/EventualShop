using System;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.ShoppingCart;

public static class DomainEvents
{
    public record CartCreated(Guid CartId, Guid CustomerId) : Event(CorrelationId: CustomerId);

    public record CartItemAdded(Guid CartId, Guid ItemId, Guid ProductId, string ProductName, decimal UnitPrice, int Quantity, string PictureUrl) : Event(CorrelationId: CartId);

    public record CartItemQuantityIncreased(Guid CartId, Guid ItemId) : Event(CorrelationId: CartId);

    public record CartItemQuantityDecreased(Guid CartId, Guid ItemId) : Event(CorrelationId: CartId);

    public record CartItemRemoved(Guid CartId, Guid ItemId) : Event(CorrelationId: CartId);

    public record CartCheckedOut(Guid CartId) : Event(CorrelationId: CartId);

    public record ShippingAddressAdded(Guid CartId, Models.Address Address) : Event(CorrelationId: CartId);

    public record BillingAddressChanged(Guid CartId, Models.Address Address) : Event(CorrelationId: CartId);

    public record CreditCardAdded(Guid CartId, Models.CreditCard CreditCard) : Event(CorrelationId: CartId);

    public record PayPalAdded(Guid CartId, Models.PayPal PayPal) : Event(CorrelationId: CartId);

    public record CartDiscarded(Guid CartId) : Event(CorrelationId: CartId);
}