using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class ConfirmEmailValidator : AbstractValidator<Commands.ConfirmEmail>
{
    public ConfirmEmailValidator()
    {
        RuleFor(request => request.UserId)
            .NotEmpty();

        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress();
    }
}