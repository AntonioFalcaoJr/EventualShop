using FluentValidation;

namespace ECommerce.Contracts.ShoppingCarts.Validators;

public class AddCartItemValidator : AbstractValidator<Command.AddCartItem>
{
    public AddCartItemValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(0);
    }
}