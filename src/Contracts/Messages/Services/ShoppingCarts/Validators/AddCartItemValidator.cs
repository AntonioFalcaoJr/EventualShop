using FluentValidation;

namespace Messages.Services.ShoppingCarts.Validators;

public class AddCartItemValidator : AbstractValidator<Commands.AddCartItem>
{
    public AddCartItemValidator()
    {
        RuleFor(cart => cart.Item.Quantity)
            .GreaterThan(0);
    }
}