using FluentValidation;

namespace Domain.Entities.ShoppingCarts
{
    public class ShoppingCartValidator : AbstractValidator<ShoppingCart>
    {
        public ShoppingCartValidator()
        {
            // RuleFor(foo => foo.Age)
            //     .GreaterThan(0);
            //
            // RuleFor(foo => foo.Name)
            //     .NotNull()
            //     .NotEmpty();
        }
    }
}