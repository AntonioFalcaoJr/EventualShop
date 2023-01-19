using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddDebitCardPayloadValidator : AbstractValidator<Payloads.AddDebitCardPayload>
{
    public AddDebitCardPayloadValidator()
    {
        RuleFor(request => request.Amount)
            .GreaterThan("0");

        RuleFor(request => request.DebitCard)
            .SetValidator(new DebitCardValidator())
            .OverridePropertyName(string.Empty);
    }
}