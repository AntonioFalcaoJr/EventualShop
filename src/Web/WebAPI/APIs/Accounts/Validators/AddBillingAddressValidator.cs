using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class AddBillingAddressValidator : AbstractValidator<Requests.AddBillingAddress>
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