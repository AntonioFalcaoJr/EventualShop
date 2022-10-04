using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class CheckOutValidator : AbstractValidator<Requests.CheckOut>
{
    public CheckOutValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();
    }
}