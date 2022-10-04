using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddBillingAddressValidator : AbstractValidator<Accounts.Requests.AddBillingAddress>
{
    public AddBillingAddressValidator()
    {
        RuleFor(request => request.AccountId)
            .NotEmpty();

        RuleFor(request => request.Address)
            .SetValidator(new AddressValidator())
            .OverridePropertyName(string.Empty);
    }
}