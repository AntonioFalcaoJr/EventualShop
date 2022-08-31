using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class LoginValidator : AbstractValidator<Requests.Login>
{
    public LoginValidator()
    {
        RuleFor(user => user.Email)
            .EmailAddress();

        RuleFor(user => user.Password)
            .NotNull()
            .NotEmpty();
    }
}