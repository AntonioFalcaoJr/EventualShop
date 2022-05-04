using Contracts.DataTransferObjects;

namespace Domain.Entities.PaymentMethods.PayPal;

public class PayPal : PaymentMethod<PayPalPaymentMethodValidator>
{
    public PayPal(Guid? id, decimal amount, string userName, string password)
        : base(id, amount)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; }
    public string Password { get; }

    public static implicit operator PayPal(Dto.PayPal payPal)
        => new(payPal.Id, payPal.Amount, payPal.UserName, payPal.Password);

    public static implicit operator Dto.PayPal(PayPal payPal)
        => new(payPal.Id, payPal.Amount, payPal.UserName, payPal.Password);
}