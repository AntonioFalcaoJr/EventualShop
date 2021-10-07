using System;
using Messages.Abstractions.Events;

namespace Messages.ShoppingCarts
{
    public static class Events
    {
        public record CartCreated(Guid CartId, Guid UserId) : Event;

        public record CartItemAdded(Guid CartId, Guid CatalogItemId, string CatalogItemName, int Quantity, decimal UnitPrice) : Event;

        public record CartItemRemoved(Guid CartId, Guid CatalogItemId) : Event;
    }
}