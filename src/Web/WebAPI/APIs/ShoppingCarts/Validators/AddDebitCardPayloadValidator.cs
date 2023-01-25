using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddDebitCardPayloadValidator : AbstractValidator<Payloads.AddDebitCardPayload>
{
    public AddDebitCardPayloadValidator()
    {
        RuleFor(request => request.Amount)
            .SetValidator(new MoneyValidator())
            .OverridePropertyName(string.Empty);

        RuleFor(request => request.DebitCard)
            .SetValidator(new DebitCardValidator())
            .OverridePropertyName(string.Empty);
    }
}