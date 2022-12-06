using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class ListShippingAddressesValidator : AbstractValidator<Requests.ListShippingAddresses>
{
    public ListShippingAddressesValidator()
    {
        RuleFor(request => request.AccountId)
            .NotEmpty();
        
        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThan(0);
    }
}