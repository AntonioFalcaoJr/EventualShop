using System;
using Messages.Abstractions.Events;

namespace Messages.ShoppingCarts
{
    public static class Events
    {
        public record CartCreated(Guid CartId, Guid CustomerId) : Event;

        public record CartItemAdded(Guid CartId, Guid ProductId, string ProductName, int Quantity, decimal UnitPrice) : Event;

        public record CartItemRemoved(Guid CartId, Guid ProductId) : Event;
    }
}