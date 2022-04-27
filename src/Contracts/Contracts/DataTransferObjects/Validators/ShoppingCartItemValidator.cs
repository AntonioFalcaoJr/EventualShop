using FluentValidation;

namespace Contracts.DataTransferObjects.Validators;

public class ShoppingCartItemValidator : AbstractValidator<Dto.ShoppingCartItem>
{
    public ShoppingCartItemValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(0);
    }
}