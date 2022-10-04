using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class CreateCartValidator : AbstractValidator<Requests.CreateCart>
{
    public CreateCartValidator()
    {
        RuleFor(request => request.CustomerId)
            .NotEmpty();
    }
}