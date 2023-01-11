using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class GetPaymentMethodDetailsValidator : AbstractValidator<Requests.GetPaymentMethodDetails>
{
    public GetPaymentMethodDetailsValidator()
    {
        RuleFor(request => request.MethodId)
            .NotEmpty();
    }
}