using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class GetCustomerShoppingCartDetailsValidator : AbstractValidator<Queries.GetCustomerShoppingCartDetails>
{
    public GetCustomerShoppingCartDetailsValidator()
    {
        RuleFor(request => request.CustomerId)
            .NotEmpty();
    }
}