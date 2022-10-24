using Domain.Abstractions.Validators;
using FluentValidation;

namespace Domain.Entities.CartItems;

public class CartItemValidator : EntityValidator<CartItem>
{
    public CartItemValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(ushort.MinValue);
    }
}