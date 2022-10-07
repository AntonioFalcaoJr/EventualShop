using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class ConfirmEmailValidator : AbstractValidator<Requests.ConfirmEmail>
{
    public ConfirmEmailValidator()
    {
        RuleFor(request => request.UserId)
            .NotEmpty();

        RuleFor(request => request.Email)
            .EmailAddress();
    }
}