using FluentValidation;

namespace Contracts.Services.ShoppingCart.Validators;

public class CreateCartValidator : AbstractValidator<Command.CreateCart>
{
    public CreateCartValidator()
    {
        RuleFor(cart => cart.CustomerId)
            .NotEqual(Guid.Empty);
    }
}