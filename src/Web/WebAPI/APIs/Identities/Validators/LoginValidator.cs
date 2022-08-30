using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class LoginValidator : AbstractValidator<Requests.Login>
{
    public LoginValidator()
    {
        RuleFor(request => request.Email)
            .EmailAddress();

        RuleFor(request => request.Password)
            .NotNull()
            .NotEmpty();
    }
}