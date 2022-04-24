using FluentValidation;

namespace ECommerce.Contracts.Identities.Validators;

public class UserRegisteredValidator : AbstractValidator<DomainEvent.UserRegistered>
{
    public UserRegisteredValidator()
    {
        RuleFor(user => user.Email)
            .EmailAddress();
    }
}