using FluentValidation;

namespace WebAPI.APIs.Payments.Validators;

public class GetPaymentValidator : AbstractValidator<Payments.Query.GetPayment>
{
    public GetPaymentValidator()
    {
        RuleFor(request => request.PaymentId)
            .NotEmpty();

    }
}