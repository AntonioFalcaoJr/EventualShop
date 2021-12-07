using FluentValidation;

namespace Domain.ValueObjects.PaymentMethods.PayPal;

public class PayPalPaymentMethodValidator : AbstractValidator<PayPalPaymentMethod>
{
    public PayPalPaymentMethodValidator()
    {
        RuleFor(card => card.Password)
            .NotNull()
            .NotEmpty();

        RuleFor(card => card.UserName)
            .NotNull()
            .NotEmpty();
    }
}