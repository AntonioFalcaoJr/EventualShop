using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class AddShippingAddressValidator : AbstractValidator<Requests.AddShippingAddress>
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