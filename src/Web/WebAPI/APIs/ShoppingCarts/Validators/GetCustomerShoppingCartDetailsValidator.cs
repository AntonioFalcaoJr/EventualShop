using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class GetCustomerShoppingCartDetailsValidator : AbstractValidator<Requests.GetCustomerShoppingCartDetails>
{
    public GetCustomerShoppingCartDetailsValidator()
    {
        RuleFor(request => request.CustomerId)
            .NotEmpty();
    }
}