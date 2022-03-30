using Domain.Entities.PaymentMethods.DebitCards;

namespace Domain.Entities.PaymentMethods.PayPal;

public class PayPalPaymentMethod : PaymentMethod<PayPalPaymentMethodValidator>
{
    public string UserName { get; init; }
    public string Password { get; init; }
}