using FluentValidation;

namespace Contracts.Services.ShoppingCart.Validators;

public class RemoveCartItemValidator : AbstractValidator<Command.RemoveCartItem>
{
    public RemoveCartItemValidator()
    {
        RuleFor(cart => cart.CartId)
            .NotEmpty();

        RuleFor(cart => cart.ItemId)
            .NotEmpty();
    }
}