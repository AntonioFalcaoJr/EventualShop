using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.Entities.CartItems;
using Domain.Entities.Coupons;
using Messages.Abstractions.Events;
using Messages.ShoppingCarts;

namespace Domain.Aggregates
{
    public class Order : AggregateRoot<Guid>
    {
        private readonly List<Coupon> _coupons = new();
        private readonly List<CartItem> _items = new();
        public Guid UserId { get; private set; }

        public decimal Total
            => Items.Sum(item
                => item.UnitPrice * item.Quantity);

        public IEnumerable<CartItem> Items
            => _items;

        public void AddItem(Guid cartId, Guid catalogItemId, string productName, int quantity, decimal unitPrice)
            => RaiseEvent(new Events.CartItemAdded(cartId, catalogItemId, productName, quantity, unitPrice));

        public void Create(Guid userId)
            => RaiseEvent(new Events.CartCreated(Guid.NewGuid(), userId));

        public void RemoveItem(Guid cartId, Guid catalogItemId)
            => RaiseEvent(new Events.CartItemRemoved(cartId, catalogItemId));

        protected override void ApplyEvent(IEvent @event)
            => When(@event as dynamic);

        private void When(Events.CartCreated @event)
            => (Id, UserId) = @event;

        private void When(Events.CartItemAdded @event)
        {
            if (_items.Exists(item => item.CatalogItemId == @event.CatalogItemId))
                IncreaseItemQuantity(@event);
            else AddNewItem(@event);
        }

        private void When(Events.CartItemRemoved @event)
            => _items.RemoveAll(item => item.CatalogItemId == @event.CatalogItemId);

        private void AddNewItem(Events.CartItemAdded @event)
        {
            var cartItem = new CartItem(
                @event.CatalogItemId,
                @event.CatalogItemName,
                @event.UnitPrice,
                @event.Quantity);

            _items.Add(cartItem);
        }

        private void IncreaseItemQuantity(Events.CartItemAdded @event)
            => _items
                .Single(item => item.CatalogItemId == @event.CatalogItemId)
                .IncreaseQuantity(@event.Quantity);

        protected sealed override bool Validate()
            => OnValidate<CartValidator, Order>();
    }
}