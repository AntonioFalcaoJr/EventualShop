using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class CheckOutValidator : AbstractValidator<Commands.CheckOut>
{
    public CheckOutValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();
    }
}