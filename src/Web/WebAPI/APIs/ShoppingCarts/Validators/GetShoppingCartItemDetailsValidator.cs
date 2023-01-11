using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class GetShoppingCartItemDetailsValidator : AbstractValidator<Queries.GetShoppingCartItemDetails>
{
    public GetShoppingCartItemDetailsValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();

        RuleFor(request => request.ItemId)
            .NotEmpty();
    }
}