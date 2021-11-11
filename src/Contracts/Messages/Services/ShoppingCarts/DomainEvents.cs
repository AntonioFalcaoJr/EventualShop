using System;
using Messages.Abstractions.Events;
using Messages.JsonConverters;
using Newtonsoft.Json;

namespace Messages.Services.ShoppingCarts;

public static class DomainEvents
{
    public record CartCreated(Guid CartId, Guid UserId) : Event;

    public record CartItemAdded(Guid CartId, Models.Item Item) : Event;

    public record CartItemQuantityIncreased(Guid CartId, Guid ProductId, int Quantity) : Event;

    public record CartItemQuantityUpdated(Guid CartId, Guid ProductId, int Quantity) : Event;

    public record CartItemRemoved(Guid CartId, Guid ProductId) : Event;

    public record CartCheckedOut(Guid CartId) : Event;

    public record ShippingAddressAdded(Guid CartId, Models.Address Address) : Event;

    public record BillingAddressChanged(Guid CartId, Models.Address Address) : Event;

    public record CreditCardAdded(Guid CartId, Models.CreditCard CreditCard) : Event;

    public record CartDiscarded(Guid CartId) : Event;
}