using FluentValidation;

namespace Contracts.Services.Identity.Validators;

public class RegisterUserValidator : AbstractValidator<Command.Register>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

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