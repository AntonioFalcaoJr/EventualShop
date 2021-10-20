using System;
using System.Collections.Generic;
using Messages.Abstractions.Events;

namespace Messages.ShoppingCarts
{
    public static class Events
    {
        public record CartCreated(Guid CartId, Guid UserId) : Event;

        public record CartItemAdded(Guid CartId, Guid CatalogItemId, string CatalogItemName, int Quantity, decimal UnitPrice) : Event;

        public record CartItemRemoved(Guid CartId, Guid CatalogItemId) : Event;

        public record CartCheckedOut(Guid CartId, IEnumerable<Models.Item> CartItems, Models.Address BillingAddress, Models.CreditCard CreditCard, Models.Address ShippingAddress) : Event;

        public record ShippingAddressAdded(Guid CartId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Event;

        public record BillingAddressChanged(Guid CartId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Event;

        public record CreditCardAdded(Guid CartId, DateOnly Expiration, string HolderName, string Number, string SecurityNumber) : Event;
    }
}