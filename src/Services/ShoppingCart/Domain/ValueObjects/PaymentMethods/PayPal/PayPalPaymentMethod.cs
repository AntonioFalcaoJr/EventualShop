namespace Domain.ValueObjects.PaymentMethods.PayPal;

public record PayPalPaymentMethod(decimal Amount, string UserName, string Password) : PaymentMethod(Amount)
{
    protected override bool Validate()
        => OnValidate<PayPalPaymentMethodValidator, PayPalPaymentMethod>();
}