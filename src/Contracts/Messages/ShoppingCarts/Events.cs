using System;
using Messages.Abstractions.Events;

namespace Messages.ShoppingCarts
{
    public static class Events
    {
        public record ShoppingCartCreated(Guid Id, Guid CustomerId) : Event;

        public record ShoppingCartItemAdded() : Event;
    }
}