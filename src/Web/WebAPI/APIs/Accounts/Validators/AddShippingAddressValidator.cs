using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class AddShippingAddressValidator : AbstractValidator<Requests.AddShippingAddress>
{
    public AddShippingAddressValidator()
    {
        RuleFor(x => x.AccountId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Address)
            .SetValidator(new AddressValidator());
    }
}