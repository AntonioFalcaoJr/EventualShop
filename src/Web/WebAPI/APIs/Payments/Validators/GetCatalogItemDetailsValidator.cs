using FluentValidation;

namespace WebAPI.APIs.Payments.Validators;

public class GetPaymentDetailsValidator : AbstractValidator<Queries.GetPaymentDetails>
{
    public GetPaymentDetailsValidator()
    {
        RuleFor(request => request.PaymentId)
            .NotEmpty();
    }
}