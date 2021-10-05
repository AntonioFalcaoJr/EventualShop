using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.Entities.CartItems;
using Messages.Abstractions.Events;
using Messages.ShoppingCarts;

namespace Domain.Aggregates
{
    public class Cart : AggregateRoot<Guid>
    {
        private readonly List<CartItem> _items = new();
        public Guid CustomerId { get; private set; }

        public decimal Total
            => Items.Sum(item
                => item.UnitPrice * item.Quantity);

        public IEnumerable<CartItem> Items
            => _items;

        public void AddItem(Guid cartId, Guid productId, string productName, int quantity, decimal unitPrice)
            => RaiseEvent(new Events.CartItemAdded(cartId, productId, productName, quantity, unitPrice));

        public void Create(Guid customerId)
            => RaiseEvent(new Events.CartCreated(Guid.NewGuid(), customerId));

        public void RemoveItem(Guid cartId, Guid productId)
            => RaiseEvent(new Events.CartItemRemoved(cartId, productId));

        protected override void ApplyEvent(IEvent @event)
            => When(@event as dynamic);

        private void When(Events.CartCreated @event)
            => (Id, CustomerId) = @event;

        private void When(Events.CartItemAdded @event)
        {
            if (_items.Exists(item => item.ProductId == @event.ProductId))
                IncreaseItemQuantity(@event);
            else AddNewItem(@event);
        }

        private void When(Events.CartItemRemoved @event)
            => _items.RemoveAll(item => item.ProductId == @event.ProductId);

        private void AddNewItem(Events.CartItemAdded @event)
        {
            var cartItem = new CartItem(
                @event.ProductId,
                @event.ProductName,
                @event.UnitPrice,
                @event.Quantity);

            _items.Add(cartItem);
        }

        private void IncreaseItemQuantity(Events.CartItemAdded @event)
            => _items
                .Single(item => item.ProductId == @event.ProductId)
                .IncreaseQuantity(@event.Quantity);

        protected sealed override bool Validate()
            => OnValidate<CartValidator, Cart>();
    }
}