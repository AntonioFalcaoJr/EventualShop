using System;
using Domain.Abstractions.ValueObjects;

namespace Domain.CreditCards
{
    public record CreditCard : ValueObject
    {
        public CreditCard(DateOnly expiration, string holderName, string number, string securityNumber)
        {
            Expiration = expiration;
            HolderName = holderName;
            Number = number;
            SecurityNumber = securityNumber;
        }

        public DateOnly Expiration { get; }
        public string HolderName { get; }
        public string Number { get; }
        public string SecurityNumber { get; }
        public bool IsDefault { get; private set; }

        public void SetDefault()
            => IsDefault = true;

        protected override bool Validate()
            => OnValidate<CreditCardValidator, CreditCard>();
    }
}