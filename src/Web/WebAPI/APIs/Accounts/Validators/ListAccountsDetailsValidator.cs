using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class ListAccountsDetailsValidator : AbstractValidator<Queries.ListAccountsDetails>
{
    public ListAccountsDetailsValidator()
    {
        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);
    }
}