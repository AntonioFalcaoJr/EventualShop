using FluentValidation;

namespace Messages.Services.Accounts.Validators;

public class AccountCreatedValidator : AbstractValidator<DomainEvents.AccountCreated>
{
    public AccountCreatedValidator()
    {
            
    }
}