using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddPaypalPayloadValidator : AbstractValidator<Payloads.AddPaypalPayload>
{
    public AddPaypalPayloadValidator()
    {
        RuleFor(request => request.Amount)
            .SetValidator(new MoneyValidator())
            .OverridePropertyName(string.Empty);

        RuleFor(request => request.PayPal)
            .SetValidator(new PayPalValidator())
            .OverridePropertyName(string.Empty);
    }
}