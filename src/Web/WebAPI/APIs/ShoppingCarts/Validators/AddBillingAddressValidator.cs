using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddBillingAddressValidator : AbstractValidator<Requests.AddBillingAddress>
{
    public AddBillingAddressValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();

        RuleFor(request => request.Address)
            .SetValidator(new AddressValidator())
            .OverridePropertyName(string.Empty);
    }
}