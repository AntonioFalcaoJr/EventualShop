using System;
using Domain.Abstractions.Events;
using Domain.Entities.CartItems;

namespace Domain.Entities.ShoppingCarts
{
    public static class Events
    {
        public record ShoppingCartCreated(Guid Id, Guid CustomerId) : DomainEvent;

        public record ShoppingCartItemAdded(ShoppingCartItem ShoppingCartItem) : DomainEvent;
    }
}