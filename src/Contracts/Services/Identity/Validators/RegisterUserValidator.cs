using FluentValidation;

namespace Contracts.Services.Identity.Validators;

public class RegisterUserValidator : AbstractValidator<Command.SignUp>
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
            .Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain 1 lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain 1 number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain 1 non alphanumeric");
    }
}