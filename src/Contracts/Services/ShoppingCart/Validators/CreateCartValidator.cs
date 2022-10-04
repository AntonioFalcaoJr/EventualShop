using FluentValidation;

namespace Contracts.Services.ShoppingCart.Validators;

public class CreateCartValidator : AbstractValidator<Command.CreateCart>
{
    public CreateCartValidator()
    {
        RuleFor(cart => cart.Id)
            .NotEmpty();

        RuleFor(cart => cart.CustomerId)
            .NotEmpty();
    }
}