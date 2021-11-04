using FluentValidation;

namespace Messages.Services.Identities.Validators;

public class UserRegisteredValidator : AbstractValidator<Events.UserRegistered>
{
    public UserRegisteredValidator()
    {
        RuleFor(user => user.Email)
            .EmailAddress();
    }
}