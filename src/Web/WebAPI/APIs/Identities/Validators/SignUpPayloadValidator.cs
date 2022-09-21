using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class SignUpPayloadValidator : AbstractValidator<Payloads.SignUp>
{
    public SignUpPayloadValidator()
    {
        RuleFor(request => request.FirstName)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(30);

        RuleFor(request => request.LastName)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(30)
            .NotEqual(request => request.FirstName);

        RuleFor(request => request.Email)
            .EmailAddress();

        RuleFor(request => request.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(16)
            .Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain 1 lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain 1 number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain 1 non alphanumeric");

        RuleFor(request => request.PasswordConfirmation)
            .Equal(request => request.Password).WithMessage("{PropertyName} must match {ComparisonProperty}");
    }
}