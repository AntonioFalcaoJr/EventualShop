using System;
using System.Collections.Generic;
using Domain.Abstractions.Entities;
using Domain.Entities.Wallets;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.CreditCards;

namespace Domain.Entities.Owners
{
    public class Owner : Entity<Guid>
    {
        private readonly List<Address> _addresses = new();

        public Owner(Guid id, int age, string email, string lastName, string name)
        {
            Id = id;
            SetAge(age);
            SetEmail(email);
            SetLastName(lastName);
            SetName(name);
        }

        public IEnumerable<Address> Addresses
            => _addresses;

        public int Age { get; private set; }
        public string Email { get; private set; }
        public string LastName { get; private set; }
        public string Name { get; private set; }
        public Wallet Wallet { get; private set; }

        public void SetName(string name)
            => Name = name;

        public void SetLastName(string lastName)
            => LastName = lastName;

        public void SetAge(int age)
            => Age = age;

        public void SetEmail(string email)
            => Email = email;

        public void AddNewAddress(Address address)
        {
            if (address is null) return;
            _addresses.Add(address);
        }

        public void RemoveAddress(Address toRemove)
            => _addresses.RemoveAll(address => address == toRemove);

        public void AddNewCreditCard(CreditCard creditCard)
        {
            Wallet ??= new(Guid.NewGuid());
            Wallet.AddNewCar(creditCard);
        }

        public void RemoveCreditCard(CreditCard creditCard)
            => Wallet.RemoveCard(creditCard);

        protected override bool Validate()
            => OnValidate<OwnerValidator, Owner>();
    }
}