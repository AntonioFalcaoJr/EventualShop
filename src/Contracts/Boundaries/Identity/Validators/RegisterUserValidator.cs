using FluentValidation;

namespace Contracts.Boundaries.Identity.Validators;

public class RegisterUserValidator : AbstractValidator<Command.RegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(account => account.FirstName)
            .Length(4, 30);

        RuleFor(account => account.LastName)
            .Length(4, 30)
            .NotEqual(user => user.FirstName);

        RuleFor(account => account.Email)
            .EmailAddress();

        RuleFor(account => account.Password)
            .MinimumLength(8)
            .MaximumLength(16)
            .Must(password => password.Any(char.IsUpper)).WithMessage("Password must contain 1 uppercase letter")
            .Must(password => password.Any(char.IsLower)).WithMessage("Password must contain 1 lowercase letter")
            .Must(password => password.Any(char.IsDigit)).WithMessage("Password must contain 1 number")
            .Must(password => password.Any(char.IsSymbol)).WithMessage("Password must contain 1 symbol");
    }
}