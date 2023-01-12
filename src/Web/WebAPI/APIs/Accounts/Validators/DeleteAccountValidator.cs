using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class DeleteAccountValidator : AbstractValidator<Commands.DeleteAccount>
{
    public DeleteAccountValidator()
    {
        RuleFor(request => request.AccountId)
            .NotNull()
            .NotEmpty();
    }
}