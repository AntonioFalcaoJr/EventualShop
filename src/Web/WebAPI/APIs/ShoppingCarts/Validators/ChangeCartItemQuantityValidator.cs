using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class ChangeCartItemQuantityValidator : AbstractValidator<Requests.ChangeCartItemQuantity>
{
    public ChangeCartItemQuantityValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();

        RuleFor(request => request.ItemId)
            .NotEmpty();

        RuleFor(request => request.Quantity)
            .GreaterThan(ushort.MinValue);
    }
}