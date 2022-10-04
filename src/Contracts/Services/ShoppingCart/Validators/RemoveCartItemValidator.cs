using FluentValidation;

namespace Contracts.Services.ShoppingCart.Validators;

public class RemoveCartItemValidator : AbstractValidator<Command.RemoveCartItem>
{
    public RemoveCartItemValidator()
    {
        RuleFor(cart => cart.Id)
            .NotEmpty();

        RuleFor(cart => cart.ItemId)
            .NotEmpty();
    }
}