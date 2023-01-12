using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class GetShoppingCartDetailsValidator : AbstractValidator<Queries.GetShoppingCartDetails>
{
    public GetShoppingCartDetailsValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();
    }
}