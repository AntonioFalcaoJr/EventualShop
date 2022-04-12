using FluentValidation;

namespace ECommerce.Contracts.Identities.Validators;

public class RegisterUserValidator : AbstractValidator<Commands.RegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(user => user.FirstName)
            .NotNull()
            .NotEmpty();

        RuleFor(user => user.Password)
            .NotNull()
            .NotEmpty()
            .Equal(user => user.PasswordConfirmation);

        RuleFor(user => user.PasswordConfirmation)
            .NotNull()
            .NotEmpty()
            .Equal(user => user.Password);
    }
}