using FluentValidation;

namespace WebAPI.APIs.Identities.Validators;

public class SignUpValidator : AbstractValidator<Commands.SignUp>
{
    public SignUpValidator()
    {
        RuleFor(request => request.Payload)
            .SetValidator(new SignUpPayloadValidator())
            .OverridePropertyName(string.Empty);
    }
}