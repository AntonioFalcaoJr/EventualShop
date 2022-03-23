using Domain.Abstractions.Validators;
using FluentValidation;

namespace Domain.Entities.ShoppingCartItems;

public class ShoppingCartItemValidator : EntityValidator<ShoppingCartItem, Guid>
{
    public ShoppingCartItemValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(0);
    }
}