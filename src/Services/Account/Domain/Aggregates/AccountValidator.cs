using Domain.Abstractions.Validators;
using Domain.Entities.Addresses;
using Domain.Entities.Profiles;
using FluentValidation;

namespace Domain.Aggregates;

public class AccountValidator : EntityValidator<Account, Guid>
{
    public AccountValidator()
    {
        RuleFor(account => account.Id)
            .NotEmpty();
        
        RuleFor(account => account.Profile)
            .SetValidator(new ProfileValidator());

        RuleForEach(account => account.Addresses)
            .SetValidator(new AddressValidator());
    }
}