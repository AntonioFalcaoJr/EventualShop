using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class ListShippingAddressesListItemsValidator : AbstractValidator<Queries.ListShippingAddressesListItems>
{
    public ListShippingAddressesListItemsValidator()
    {
        RuleFor(request => request.AccountId)
            .NotEmpty();
        
        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);
    }
}