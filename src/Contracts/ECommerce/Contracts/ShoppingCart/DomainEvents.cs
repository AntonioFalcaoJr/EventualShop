using System;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.ShoppingCart;

public static class DomainEvents
{
    public record CartCreated(Guid CartId, Guid UserId) : Event;

    public record CartItemAdded(Guid CartId, Guid ItemId, Guid ProductId, string ProductName, decimal UnitPrice, int Quantity, string PictureUrl) : Event;

    public record CartItemQuantityIncreased(Guid CartId, Guid ItemId, int Quantity) : Event;

    public record CartItemQuantityDecreased(Guid CartId, Guid ItemId, int Quantity) : Event;

    public record CartItemQuantityUpdated(Guid CartId, Guid ItemId, int Quantity) : Event;

    public record CartItemRemoved(Guid CartId, Guid ItemId) : Event;

    public record CartCheckedOut(Guid CartId) : Event;

    public record ShippingAddressAdded(Guid CartId, Models.Address Address) : Event;

    public record BillingAddressChanged(Guid CartId, Models.Address Address) : Event;

    public record CreditCardAdded(Guid CartId, Models.CreditCard CreditCard) : Event;

    public record PayPalAdded(Guid CartId, Models.PayPal PayPal) : Event;

    public record CartDiscarded(Guid CartId) : Event;
}