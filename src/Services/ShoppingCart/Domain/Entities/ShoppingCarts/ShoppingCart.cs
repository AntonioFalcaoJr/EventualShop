using System;
using System.Collections.Generic;
using Domain.Abstractions.Aggregates;
using Domain.Entities.CartItems;
using Messages.Abstractions.Events;
using Messages.ShoppingCarts;

namespace Domain.Entities.ShoppingCarts
{
    public class ShoppingCart : AggregateRoot<Guid>
    {
        private readonly List<ShoppingCartItem> _items = new();
        public IReadOnlyCollection<ShoppingCartItem> Items => _items;
        public Guid CustomerId { get; private set; }
        public decimal Total { get; private set; }

        public void AddItem(ShoppingCartItem item)
            => RaiseEvent(new Events.ShoppingCartItemAdded());

        public void Create(Guid customerId)
            => RaiseEvent(new Events.ShoppingCartCreated(Guid.NewGuid(), customerId));

        protected override void ApplyEvent(IEvent @event)
            => When(@event as dynamic);

        private void When(Events.ShoppingCartCreated @event)
            => (Id, CustomerId) = @event;

        private void When(Events.ShoppingCartItemAdded @event)
        {
            // var shoppingCartItem = @event.ShoppingCartItem;
            // IncreaseTotal(shoppingCartItem.UnitPrice, shoppingCartItem.Quantity);
            // _items.Add(shoppingCartItem);
        }

        private void IncreaseTotal(decimal unitPrice, int quantity)
            => Total += unitPrice * quantity;

        private void DecreaseTotal(decimal unitPrice, int quantity)
            => Total -= unitPrice * quantity;

        protected sealed override bool Validate()
            => OnValidate<ShoppingCartValidator, ShoppingCart>();
    }
}