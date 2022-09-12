using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class RegisterUserValidator : AbstractValidator<Requests.RegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Payload)
            .SetValidator(new RegisterUserPayloadValidator())
            .OverridePropertyName(string.Empty);
    }
}