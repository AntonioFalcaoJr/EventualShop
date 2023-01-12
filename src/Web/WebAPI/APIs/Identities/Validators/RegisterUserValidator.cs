using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class RegisterUserValidator : AbstractValidator<Commands.RegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Payload)
            .SetValidator(new SignUpPayloadValidator())
            .OverridePropertyName(string.Empty);
    }
}