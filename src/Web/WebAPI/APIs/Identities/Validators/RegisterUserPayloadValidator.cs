using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class RegisterUserPayloadValidator : AbstractValidator<Payloads.RegisterUser>
{
    public RegisterUserPayloadValidator()
    {
        RuleFor(account => account.FirstName)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(30);

        RuleFor(account => account.LastName)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(30)
            .NotEqual(user => user.FirstName);

        RuleFor(account => account.Email)
            .EmailAddress();

        RuleFor(account => account.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(16)
            .Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain 1 lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain 1 number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain 1 non alphanumeric");

        RuleFor(account => account.PasswordConfirmation)
            .Equal(account => account.Password).WithMessage("{PropertyName} must match {ComparisonProperty}");
    }
}