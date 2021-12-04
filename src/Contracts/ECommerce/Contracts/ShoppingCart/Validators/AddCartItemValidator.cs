using FluentValidation;

namespace ECommerce.Contracts.ShoppingCart.Validators;

public class AddCartItemValidator : AbstractValidator<Commands.AddCartItem>
{
    public AddCartItemValidator()
    {
        RuleFor(cart => cart.Item.Quantity)
            .GreaterThan(0);
    }
}