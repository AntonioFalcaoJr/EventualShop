using System;
using Domain.Abstractions.Validators;
using Domain.CreditCards;
using FluentValidation;

namespace Domain.Wallets
{
    public class WalletValidator : EntityValidator<Wallet, Guid>
    {
        public WalletValidator()
        {
            RuleFor(wallet => wallet.Id)
                .NotEqual(Guid.Empty);

            RuleForEach(wallet => wallet.CreditCards)
                .NotNull()
                .SetValidator(new CreditCardValidator());
        }
    }
}