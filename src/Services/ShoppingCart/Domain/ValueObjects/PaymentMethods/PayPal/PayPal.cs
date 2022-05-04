using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.PaymentMethods.PayPal;

public record PayPal(decimal Amount, string UserName, string Password) : PaymentMethod<PayPalValidator>(Amount)
{
    public static implicit operator PayPal(Dto.PayPal creditCard) 
        => new(creditCard.Amount, creditCard.UserName, creditCard.Password);

    // TODO - Review this Identifier default.
    public static implicit operator Dto.PayPal(PayPal creditCard) 
        => new(default, creditCard.Amount, creditCard.UserName, creditCard.Password);
}