using FluentValidation;

namespace Domain.Entities.ShoppingCarts
{
    public class Validator : AbstractValidator<ShoppingCart>
    {
        public Validator()
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