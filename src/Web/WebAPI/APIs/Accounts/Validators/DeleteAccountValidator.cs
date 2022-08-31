using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class DeleteAccountValidator : AbstractValidator<Requests.DeleteAccount>
{
    public DeleteAccountValidator()
    {
        RuleFor(account => account.AccountId)
            .NotNull()
            .NotEmpty();
    }
}