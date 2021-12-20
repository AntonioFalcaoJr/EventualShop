using FluentValidation;

namespace ECommerce.Contracts.ShoppingCart.Validators;

public class GetShoppingCartValidator : AbstractValidator<Queries.GetShoppingCart>
{
    public GetShoppingCartValidator()
    {
        RuleFor(cart => cart.UserId)
            .NotEqual(Guid.Empty);
    }
}