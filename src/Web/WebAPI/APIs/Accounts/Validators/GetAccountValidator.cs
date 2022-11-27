using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class GetAccountValidator : AbstractValidator<Requests.GetAccount>
{
    public GetAccountValidator()
    {
        RuleFor(request => request.AccountId)
            .NotEmpty();
    }
}