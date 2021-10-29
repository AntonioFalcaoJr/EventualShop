using System;
using Domain.Abstractions.Validators;
using Domain.ValueObjects.Profiles;

namespace Domain.Aggregates;

public class AccountValidator : EntityValidator<Account, Guid>
{
    public AccountValidator()
    {
        RuleFor(account => account.Profile)
            .SetValidator(new ProfileValidator());
    }
}