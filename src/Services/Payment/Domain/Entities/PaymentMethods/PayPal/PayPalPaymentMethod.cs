namespace Domain.Entities.PaymentMethods.PayPal;

public class PayPalPaymentMethod : PaymentMethod<PayPalPaymentMethodValidator>
{
    public PayPalPaymentMethod(Guid id, decimal amount, string userName, string password)
        : base(id, amount)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; }
    public string Password { get; }
}