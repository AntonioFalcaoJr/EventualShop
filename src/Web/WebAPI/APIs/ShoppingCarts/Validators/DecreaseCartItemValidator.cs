using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class DecreaseCartItemValidator : AbstractValidator<Requests.DecreaseCartItem>
{
    public DecreaseCartItemValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();

        RuleFor(request => request.ItemId)
            .NotEmpty();
    }
}