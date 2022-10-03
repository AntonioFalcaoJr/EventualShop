using Domain.Abstractions.Validators;
using FluentValidation;

namespace Domain.Aggregates;

public class PaymentValidator : EntityValidator<Payment, Guid>
{
    public PaymentValidator()
    {
        RuleFor(payment => payment.Id)
            .NotEmpty();
    }
}