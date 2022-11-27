using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class ListAccountsValidator : AbstractValidator<Requests.ListAccounts>
{
    public ListAccountsValidator()
    {
        RuleFor(request => request.Limit)
            .GreaterThan(ushort.MinValue);
        
        RuleFor(request => request.Offset)
            .GreaterThan(ushort.MinValue);
    }
}