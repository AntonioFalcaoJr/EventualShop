using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class GetAccountValidator : AbstractValidator<Requests.GetAccountDetails>
{
    public GetAccountValidator()
    {
        RuleFor(request => request.AccountId)
            .NotEmpty();
    }
}