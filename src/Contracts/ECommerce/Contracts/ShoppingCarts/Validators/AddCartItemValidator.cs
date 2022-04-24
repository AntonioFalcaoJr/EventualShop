using FluentValidation;

namespace ECommerce.Contracts.ShoppingCarts.Validators;

public class AddCartItemValidator : AbstractValidator<Command.AddCartItem>
{
    public AddCartItemValidator()
    {
        RuleFor(cart => cart.Item.Quantity)
            .GreaterThan(0);
    }
}