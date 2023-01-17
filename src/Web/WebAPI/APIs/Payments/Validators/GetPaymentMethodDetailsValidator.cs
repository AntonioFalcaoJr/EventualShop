using FluentValidation;

namespace WebAPI.APIs.Payments.Validators;

public class GetPaymentMethodDetailsValidator : AbstractValidator<Queries.GetPaymentMethodDetails>
{
    public GetPaymentMethodDetailsValidator()
    {
        RuleFor(request => request.PaymentId)
            .NotEmpty();
    }
}