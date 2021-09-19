using System;
using System.Collections.Generic;
using Domain.Abstractions.Entities;
using Domain.ValueObjects.Cards;

namespace Domain.Entities.Wallets
{
    public class Wallet : Entity<Guid>
    {
        private readonly List<CreditCard> _creditCards = new();

        public Wallet()
        {
            Id = Guid.NewGuid();
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