using FluentValidation;

namespace WebAPI.APIs.Shopping.Validators;

public class GetPaymentMethodDetailsValidator : AbstractValidator<Queries.GetPaymentMethodDetails>
{
    public GetPaymentMethodDetailsValidator()
    {
        RuleFor(request => request.MethodId)
            .NotEmpty();
    }
}