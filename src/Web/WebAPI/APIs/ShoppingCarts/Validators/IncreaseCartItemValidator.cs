using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class IncreaseCartItemValidator : AbstractValidator<Requests.IncreaseCartItem>
{
    public IncreaseCartItemValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();

        RuleFor(request => request.ItemId)
            .NotEmpty();
    }
}