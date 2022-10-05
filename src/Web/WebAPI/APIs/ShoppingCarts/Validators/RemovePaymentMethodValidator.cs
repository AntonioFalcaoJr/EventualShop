using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class RemovePaymentMethodValidator : AbstractValidator<Requests.RemovePaymentMethod>
{
    public RemovePaymentMethodValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();

        RuleFor(request => request.MethodId)
            .NotEmpty();
    }
}