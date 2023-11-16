using FluentValidation;

namespace WebAPI.APIs.Shopping.Validators;

public class ChangeCartItemQuantityValidator : AbstractValidator<Commands.ChangeCartItemQuantity>
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