using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class ListShippingAddressesValidator : AbstractValidator<Requests.ListShippingAddresses>
{
    public ListShippingAddressesValidator()
    {
        RuleFor(request => request.AccountId)
            .NotEmpty();
    }
}