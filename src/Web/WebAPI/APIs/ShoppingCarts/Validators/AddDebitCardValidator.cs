using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddDebitCardValidator : AbstractValidator<Commands.AddDebitCard>
{
    public AddDebitCardValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();

        RuleFor(request => request.Amount)
            .GreaterThan("0");

        RuleFor(request => request.DebitCard)
            .SetValidator(new DebitCardValidator())
            .OverridePropertyName(string.Empty);
    }
}