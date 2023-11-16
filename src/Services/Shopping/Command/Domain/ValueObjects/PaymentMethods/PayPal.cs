using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.PaymentMethods;

public record PayPal(string Email, string Password) : IPaymentMethod
{
    public static implicit operator PayPal(Dto.PayPal payPal)
        => new(payPal.Email, payPal.Password);

    public static implicit operator Dto.PayPal(PayPal payPal)
        => new(payPal.Email, payPal.Password);
}