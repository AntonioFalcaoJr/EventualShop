using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class GetAccountValidator : AbstractValidator<Queries.GetAccountDetails>
{
    public GetAccountValidator()
    {
        RuleFor(request => request.AccountId)
            .NotEmpty();
    }
}