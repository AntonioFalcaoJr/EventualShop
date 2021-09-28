using FluentValidation;

namespace Messages.Identities.Validators
{
    public class UserRegisteredValidator : AbstractValidator<Events.UserRegistered>
    {
        public UserRegisteredValidator()
        {
            RuleFor(user => user.Email)
                .EmailAddress();
        }
    }
}