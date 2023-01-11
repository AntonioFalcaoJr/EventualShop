using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class ListShoppingCartItemsListItemsValidator : AbstractValidator<Requests.ListShoppingCartItemsListItems>
{
    public ListShoppingCartItemsListItemsValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();

        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);
    }
}