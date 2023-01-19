using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddCreditCardPayloadValidator : AbstractValidator<Payloads.AddCreditCardPayload>
{
    public AddCreditCardPayloadValidator()
    {
        RuleFor(request => request.Amount)
            .GreaterThan("0");

        RuleFor(request => request.CreditCard)
            .SetValidator(new CreditCardValidator())
            .OverridePropertyName(string.Empty);
    }
}