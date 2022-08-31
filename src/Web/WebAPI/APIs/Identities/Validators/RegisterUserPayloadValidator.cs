using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class RegisterUserPayloadValidator : AbstractValidator<Payloads.RegisterUser>
{
    public RegisterUserPayloadValidator()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(30);

        RuleFor(user => user.LastName)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(30)
            .NotEqual(user => user.FirstName);

        RuleFor(user => user.Email)
            .EmailAddress();

        RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(16)
            .Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain 1 lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain 1 number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain 1 non alphanumeric");

        RuleFor(user => user.PasswordConfirmation)
            .Equal(user => user.Password).WithMessage("{PropertyName} must match {ComparisonProperty}");
    }
}