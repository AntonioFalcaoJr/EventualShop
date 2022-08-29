using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class CreateAccountPayloadValidator : AbstractValidator<Payloads.CreateAccount>
{
    public CreateAccountPayloadValidator()
    {
        RuleFor(account => account.Email)
            .EmailAddress();

        RuleFor(account => account.Password)
            .NotEmpty()
            .Length(8, 16);

        RuleFor(account => account.PasswordConfirmation)
            .NotEmpty()
            .Equal(account => account.Password)
            .Length(8, 16);
    }
}