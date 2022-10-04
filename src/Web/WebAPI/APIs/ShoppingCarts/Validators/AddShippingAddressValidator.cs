using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddShippingAddressValidator : AbstractValidator<Accounts.Requests.AddShippingAddress>
{
    public AddShippingAddressValidator()
    {
        RuleFor(request => request.AccountId)
            .NotEmpty();

        RuleFor(request => request.Address)
            .SetValidator(new AddressValidator())
            .OverridePropertyName(string.Empty);
    }
}