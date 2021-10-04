using FluentValidation;

namespace Messages.ShoppingCarts.Validators
{
    public class AddCartItemValidator : AbstractValidator<Commands.AddCartItem>
    {
        public AddCartItemValidator()
        {
            
        }
    }
}