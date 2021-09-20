using System;
using Domain.Abstractions.Validators;
using Domain.Entities.Owners;
using Domain.Entities.Users;

namespace Domain.Aggregates.Accounts
{
    public class AccountValidator : EntityValidator<Account, Guid>
    {
        public AccountValidator()
        {
            RuleFor(account => account.Owner)
                .SetValidator(new OwnerValidator());

            RuleFor(account => account.User)
                .SetValidator(new UserValidator());
        }
    }
}