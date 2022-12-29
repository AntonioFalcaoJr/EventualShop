using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class ListAccountsValidator : AbstractValidator<Requests.ListAccounts>
{
    public ListAccountsValidator()
    {
        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);
    }
}