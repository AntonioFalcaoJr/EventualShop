using System;
using System.Collections.Generic;
using Domain.Abstractions.Entities;
using Domain.CreditCards;

namespace Domain.Wallets
{
    public class Wallet : Entity<Guid>
    {
        private readonly List<CreditCard> _creditCards = new();

        public Wallet(Guid id)
        {
            Id = id;
        }

        public IEnumerable<CreditCard> CreditCards
            => _creditCards;

        public void AddNewCar(CreditCard creditCard)
            => _creditCards.Add(creditCard);

        public void RemoveCard(CreditCard toRemove)
            => _creditCards.RemoveAll(creditCard => creditCard == toRemove);

        protected override bool Validate()
            => OnValidate<WalletValidator, Wallet>();
    }
}