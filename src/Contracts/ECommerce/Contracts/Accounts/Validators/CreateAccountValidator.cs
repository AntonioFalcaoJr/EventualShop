using FluentValidation;

namespace ECommerce.Contracts.Accounts.Validators;

public class CreateAccountValidator : AbstractValidator<Command.CreateAccount>
{
    public CreateAccountValidator()
    {
        RuleFor(account => account.UserId)
            .NotEqual(default(Guid));
    }
}