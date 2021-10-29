using FluentValidation;

namespace Messages.Accounts.Validators;

public class AccountCreatedValidator : AbstractValidator<Events.AccountCreated>
{
    public AccountCreatedValidator()
    {
            
    }
}