using FluentValidation;

namespace Domain.Aggregates;

public class PaymentValidator : AbstractValidator<Payment>
{
    public PaymentValidator()
    {
        RuleFor(payment => payment.Id)
            .NotEmpty();
    }
}