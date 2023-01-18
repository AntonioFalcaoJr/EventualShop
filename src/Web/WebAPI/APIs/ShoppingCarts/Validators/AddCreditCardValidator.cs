using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddCreditCardValidator : AbstractValidator<Commands.AddCreditCard>
{
    public AddCreditCardValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();

        RuleFor(request => request.Amount)
            .GreaterThan("0");

        RuleFor(request => request.CreditCard)
            .SetValidator(new CreditCardValidator())
            .OverridePropertyName(string.Empty);
    }
}