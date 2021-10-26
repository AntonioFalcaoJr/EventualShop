using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.Entities.CartItems;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.CreditCards;
using Messages.Abstractions.Events;
using Messages.ShoppingCarts;

namespace Domain.Aggregates
{
    public class Cart : AggregateRoot<Guid>
    {
        private readonly List<CartItem> _items = new();
        public Guid UserId { get; private set; }
        public bool IsCheckedOut { get; private set; }
        public Address ShippingAddress { get; private set; }
        public Address BillingAddress { get; private set; }
        public CreditCard CreditCard { get; private set; }
        private bool ShippingAndBillingAddressesAreSame { get; set; } = true;

        public decimal Total
            => Items.Sum(item
                => item.UnitPrice * item.Quantity);

        public IEnumerable<CartItem> Items
            => _items;

        public void Handle(Commands.AddCartItem cmd)
            => RaiseEvent(new Events.CartItemAdded(Id, cmd.ProductId, cmd.ProductName, cmd.Quantity, cmd.UnitPrice));
       
        public void Handle(Commands.CreateCart cmd)
            => RaiseEvent(new Events.CartCreated(Guid.NewGuid(), cmd.CustomerId));
        
        public void Handle(Commands.AddCreditCard cmd)
            => RaiseEvent(new Events.CreditCardAdded(Id, cmd.Expiration, cmd.HolderName, cmd.Number, cmd.SecurityNumber));

        public void Handle(Commands.AddShippingAddress cmd)
            => RaiseEvent(new Events.ShippingAddressAdded(Id, cmd.City, cmd.Country, cmd.Number, cmd.State, cmd.Street, cmd.ZipCode));
        
        public void Handle(Commands.ChangeBillingAddress cmd)
            => RaiseEvent(new Events.BillingAddressChanged(Id, cmd.City, cmd.Country, cmd.Number, cmd.State, cmd.Street, cmd.ZipCode));

        public void Handle(Commands.RemoveCartItem cmd)
            => RaiseEvent(new Events.CartItemRemoved(Id, cmd.ProductId));

        protected override void ApplyEvent(IEvent @event)
            => When(@event as dynamic);

        private void When(Events.CartCreated @event)
            => (Id, UserId) = @event;

        private void When(Events.CartCheckedOut _)
            => IsCheckedOut = true;

        private void When(Events.CartItemAdded @event)
        {
            if (_items.Exists(item => item.CatalogItemId == @event.CatalogItemId))
                IncreaseItemQuantity(@event);
            else AddNewItem(@event);
        }

        private void When(Events.CartItemRemoved @event)
            => _items.RemoveAll(item
                => item.CatalogItemId == @event.CatalogItemId);

        private void AddNewItem(Events.CartItemAdded @event)
            => _items.Add(
                new CartItem(
                    @event.CatalogItemId,
                    @event.CatalogItemName,
                    @event.UnitPrice,
                    @event.Quantity));

        private void When(Events.CreditCardAdded @event)
            => CreditCard =
                new CreditCard
                {
                    Expiration = @event.Expiration,
                    HolderName = @event.HolderName,
                    Number = @event.Number,
                    SecurityNumber = @event.SecurityNumber
                };

        private void When(Events.ShippingAddressAdded @event)
        {
            ShippingAddress = new Address
            {
                City = @event.City,
                Country = @event.Country,
                Number = @event.Number,
                State = @event.State,
                Street = @event.Street,
                ZipCode = @event.ZipCode
            };

            if (ShippingAndBillingAddressesAreSame)
                BillingAddress = ShippingAddress;
        }

        private void When(Events.BillingAddressChanged @event)
        {
            BillingAddress = new Address
            {
                City = @event.City,
                Country = @event.Country,
                Number = @event.Number,
                State = @event.State,
                Street = @event.Street,
                ZipCode = @event.ZipCode
            };

            ShippingAndBillingAddressesAreSame = false;
        }

        private void IncreaseItemQuantity(Events.CartItemAdded @event)
            => _items
                .Single(item => item.CatalogItemId == @event.CatalogItemId)
                .IncreaseQuantity(@event.Quantity);

        protected sealed override bool Validate()
            => OnValidate<CartValidator, Cart>();
    }
}