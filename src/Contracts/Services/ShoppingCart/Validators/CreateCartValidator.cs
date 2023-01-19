using FluentValidation;

namespace Contracts.Services.ShoppingCart.Validators;

public class CreateCartValidator : AbstractValidator<Command.CreateCart>
{
    public CreateCartValidator()
    {
        RuleFor(cart => cart.CustomerId)
            .NotEmpty();

        RuleFor(cart => cart.Currency)
            .NotEmpty();
    }
}