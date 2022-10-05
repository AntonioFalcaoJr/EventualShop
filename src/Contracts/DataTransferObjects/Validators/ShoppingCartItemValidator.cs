using FluentValidation;

namespace Contracts.DataTransferObjects.Validators;

public class ShoppingCartItemValidator : AbstractValidator<Dto.CartItem>
{
    public ShoppingCartItemValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(ushort.MaxValue);
    }
}