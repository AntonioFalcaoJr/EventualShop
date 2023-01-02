using Domain.Entities.Addresses;
using Domain.Entities.Profiles;
using FluentValidation;

namespace Domain.Aggregates;

public class AccountValidator : AbstractValidator<Account>
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