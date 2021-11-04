using FluentValidation;

namespace Messages.Services.Accounts.Validators;

public class AccountCreatedValidator : AbstractValidator<Events.AccountCreated>
{
    public AccountCreatedValidator()
    {
            
    }
}