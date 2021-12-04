using FluentValidation;

namespace ECommerce.Contracts.Account.Validators;

public class AccountCreatedValidator : AbstractValidator<DomainEvents.AccountCreated>
{
    public AccountCreatedValidator()
    {
            
    }
}