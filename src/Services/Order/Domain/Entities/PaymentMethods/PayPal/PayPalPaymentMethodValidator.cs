using Domain.Entities.PaymentMethods.DebitCards;
using FluentValidation;

namespace Domain.Entities.PaymentMethods.PayPal;

public class PayPalPaymentMethodValidator : AbstractValidator<PayPalPaymentMethod>
{
    public PayPalPaymentMethodValidator()
    {

    }
}