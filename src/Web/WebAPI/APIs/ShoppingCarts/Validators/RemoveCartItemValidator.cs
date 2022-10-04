using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class RemoveCartItemValidator : AbstractValidator<Requests.RemoveCartItem>
{
    public RemoveCartItemValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();

        RuleFor(request => request.ItemId)
            .NotEmpty();
    }
}