using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class CreateAccountValidator : AbstractValidator<Requests.CreateAccount>
{
    public CreateAccountValidator()
    {
        RuleFor(account => account.Payload)
            .SetValidator(new CreateAccountPayloadValidator())
            .OverridePropertyName(string.Empty);
    }
}