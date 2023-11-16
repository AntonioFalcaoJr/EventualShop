using FluentValidation;

namespace Contracts.Boundaries.Identity.Validators;

public class ConfirmEmailValidator : AbstractValidator<Command.ConfirmEmail>
{
    public ConfirmEmailValidator()
    {
        RuleFor(user => user.UserId)
            .NotEmpty();

        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress();
    }
}