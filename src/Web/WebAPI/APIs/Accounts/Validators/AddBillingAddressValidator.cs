using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.Accounts.Validators;

public class AddBillingAddressValidator : AbstractValidator<Requests.AddBillingAddress>
{
    public AddBillingAddressValidator()
    {
        RuleFor(account => account.AccountId)
            .NotNull()
            .NotEmpty();

        RuleFor(account => account.Address)
            .SetValidator(new AddressValidator())
            .OverridePropertyName(string.Empty);
    }
}