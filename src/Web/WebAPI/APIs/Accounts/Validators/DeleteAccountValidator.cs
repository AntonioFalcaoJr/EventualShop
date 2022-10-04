using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class DeleteAccountValidator : AbstractValidator<Requests.DeleteAccount>
{
    public DeleteAccountValidator()
    {
        RuleFor(request => request.AccountId)
            .NotNull()
            .NotEmpty();
    }
}