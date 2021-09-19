using System;
using Domain.Abstractions.Validators;
using Domain.Entities.Owners;
using FluentValidation;

namespace Domain.Entities.Accounts
{
    public class AccountValidator : EntityValidator<Account, Guid>
    {
        public AccountValidator()
        {
            RuleFor(account => account.Owner)
                .SetValidator(new OwnerValidator());

            RuleFor(account => account.Password)
                .NotNull()
                .NotEmpty()
                .Equal(account => account.PasswordConfirmation);

            RuleFor(account => account.PasswordConfirmation)
                .NotNull()
                .NotEmpty()
                .Equal(account => account.Password);

            RuleFor(account => account.UserName)
                .NotNull()
                .NotEmpty();
        }
    }
}