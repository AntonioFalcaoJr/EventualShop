using FluentValidation;

namespace Domain.Entities.CartItems;

public class CartItemValidator : AbstractValidator<CartItem>
{
    public CartItemValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(ushort.MinValue);
    }
}