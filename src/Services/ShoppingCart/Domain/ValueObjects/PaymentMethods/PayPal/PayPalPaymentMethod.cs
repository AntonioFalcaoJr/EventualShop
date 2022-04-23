namespace Domain.ValueObjects.PaymentMethods.PayPal;

public record PayPalPaymentMethod(decimal Amount, string UserName, string Password) : PaymentMethod<PayPalPaymentMethodValidator>(Amount) { }