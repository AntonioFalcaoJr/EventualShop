using FluentValidation;

namespace ECommerce.Contracts.Common.Validators;

public class ShoppingCartItemValidator : AbstractValidator<Models.ShoppingCartItem>
{
    public ShoppingCartItemValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(0);
    }
}