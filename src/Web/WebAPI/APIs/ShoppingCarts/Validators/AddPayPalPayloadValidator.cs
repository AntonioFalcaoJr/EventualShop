using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddPaypalPayloadValidator : AbstractValidator<Payloads.AddPaypalPayload>
{
    public AddPaypalPayloadValidator()
    {
        RuleFor(request => request.Amount)
            .GreaterThan("0");

        RuleFor(request => request.PayPal)
            .SetValidator(new PayPalValidator())
            .OverridePropertyName(string.Empty);
    }
}