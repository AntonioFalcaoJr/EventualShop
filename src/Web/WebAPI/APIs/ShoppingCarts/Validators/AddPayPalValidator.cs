using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddPayPalValidator : AbstractValidator<Commands.AddPayPal>
{
    public AddPayPalValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();

        RuleFor(request => request.Amount)
            .GreaterThan(0);

        RuleFor(request => request.PayPal)
            .SetValidator(new PayPalValidator())
            .OverridePropertyName(string.Empty);
    }
}