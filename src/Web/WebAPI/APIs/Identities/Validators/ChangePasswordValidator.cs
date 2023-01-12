using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class ChangePasswordValidator : AbstractValidator<Commands.ChangePassword>
{
    public ChangePasswordValidator()
    {
        RuleFor(request => request.UserId)
            .NotEmpty();

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