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
        public decimal Total { get; private set; }

        public IEnumerable<CartItem> Items
            => _items;

        public void AddItem(Guid shoppingCartId, Guid productId, string productName, int quantity, decimal unitPrice)
            => RaiseEvent(new Events.CartItemAdded(shoppingCartId, productId, productName, quantity, unitPrice));

        public void Create(Guid customerId)
            => RaiseEvent(new Events.CartCreated(Guid.NewGuid(), customerId));

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

        private void AddNewItem(Events.CartItemAdded @event)
        {
            var shoppingCartItem = new CartItem(
                @event.ProductId,
                @event.ProductName,
                @event.UnitPrice,
                @event.Quantity);

            _items.Add(shoppingCartItem);

            IncreaseTotal(shoppingCartItem.UnitPrice, shoppingCartItem.Quantity);
        }

        private void IncreaseItemQuantity(Events.CartItemAdded @event)
        {
            _items
                .Single(item => item.ProductId == @event.ProductId)
                .IncreaseQuantity(@event.Quantity);

            IncreaseTotal(@event.UnitPrice, @event.Quantity);
        }

        private void IncreaseTotal(decimal unitPrice, int quantity)
            => Total += unitPrice * quantity;

        private void DecreaseTotal(decimal unitPrice, int quantity)
            => Total -= unitPrice * quantity;

        protected sealed override bool Validate()
            => OnValidate<CartValidator, Cart>();
    }
}