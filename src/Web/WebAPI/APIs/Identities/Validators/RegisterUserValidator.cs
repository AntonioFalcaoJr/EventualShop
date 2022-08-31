using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class RegisterUserValidator : AbstractValidator<Requests.RegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(account => account.Payload)
            .SetValidator(new RegisterUserPayloadValidator())
            .OverridePropertyName(string.Empty);
    }
}