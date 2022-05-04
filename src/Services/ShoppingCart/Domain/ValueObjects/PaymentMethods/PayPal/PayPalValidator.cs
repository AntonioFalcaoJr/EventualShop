using FluentValidation;

namespace Domain.ValueObjects.PaymentMethods.PayPal;

public class PayPalValidator : AbstractValidator<PayPal>
{
    public PayPalValidator()
    {
        RuleFor(card => card.Password)
            .NotNull()
            .NotEmpty();

        RuleFor(card => card.UserName)
            .NotNull()
            .NotEmpty();
    }
}