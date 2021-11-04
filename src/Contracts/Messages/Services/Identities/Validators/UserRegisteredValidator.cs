using FluentValidation;

namespace Messages.Services.Identities.Validators;

public class UserRegisteredValidator : AbstractValidator<DomainEvents.UserRegistered>
{
    public UserRegisteredValidator()
    {
        RuleFor(user => user.Email)
            .EmailAddress();
    }
}