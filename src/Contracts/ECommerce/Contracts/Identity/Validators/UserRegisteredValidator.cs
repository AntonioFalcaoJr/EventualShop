using FluentValidation;

namespace ECommerce.Contracts.Identity.Validators;

public class UserRegisteredValidator : AbstractValidator<DomainEvents.UserRegistered>
{
    public UserRegisteredValidator()
    {
        RuleFor(user => user.Email)
            .EmailAddress();
    }
}