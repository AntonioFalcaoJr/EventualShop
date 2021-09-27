using System;
using FluentValidation;

namespace Messages.Accounts.Validators
{
    public class CreateAccountValidator : AbstractValidator<Commands.CreateAccount>
    {
        public CreateAccountValidator()
        {
            RuleFor(account => account.AccountId)
                .NotEqual(default(Guid));

            RuleFor(account => account.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(account => account.FirstName)
                .NotNull()
                .NotEmpty();
        }
    }
}