using System;
using Domain.Abstractions.Validators;
using Domain.Entities.Wallets;
using Domain.ValueObjects.Addresses;
using FluentValidation;

namespace Domain.Entities.Owners
{
    public class OwnerValidator : EntityValidator<Owner, Guid>
    {
        public OwnerValidator()
        {
            RuleForEach(owner => owner.Addresses)
                .NotNull()
                .SetValidator(new AddressValidator());

            RuleFor(owner => owner.Age)
                .InclusiveBetween(18, 80);

            RuleFor(owner => owner.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(owner => owner.LastName)
                .NotNull()
                .NotEmpty()
                .NotEqual(owner => owner.Name);

            RuleFor(owner => owner.Name)
                .NotNull()
                .NotEmpty()
                .NotEqual(owner => owner.LastName);

            RuleFor(owner => owner.Wallet)
                .SetValidator(new WalletValidator());
        }
    }
}