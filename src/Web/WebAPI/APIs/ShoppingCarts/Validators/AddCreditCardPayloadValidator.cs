using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddCreditCardPayloadValidator : AbstractValidator<Payloads.AddCreditCardPayload>
{
    public AddCreditCardPayloadValidator()
    {
        RuleFor(request => request.Amount)
            .SetValidator(new MoneyValidator())
            .OverridePropertyName(string.Empty);

        RuleFor(request => request.CreditCard)
            .SetValidator(new CreditCardValidator())
            .OverridePropertyName(string.Empty);
    }
}