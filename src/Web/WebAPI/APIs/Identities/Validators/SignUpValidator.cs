using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class SignUpValidator : AbstractValidator<Commands.RegisterUser>
{
    public SignUpValidator()
    {
        RuleFor(request => request.Payload)
            .SetValidator(new SignUpPayloadValidator())
            .OverridePropertyName(string.Empty);
    }
}