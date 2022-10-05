using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class VerifyEmailValidator : AbstractValidator<Requests.VerifyEmail>
{
    public VerifyEmailValidator()
    {
        RuleFor(request => request.UserId)
            .NotEmpty();

        RuleFor(request => request.Email)
            .EmailAddress();
    }
}