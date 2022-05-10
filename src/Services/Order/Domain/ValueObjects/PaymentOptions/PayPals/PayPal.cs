using Contracts.DataTransferObjects;
using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.PaymentOptions.PayPals;

public record PayPal(string UserName, string Password) : ValueObject<PayPalValidator>, IPaymentOption
{
    public static implicit operator PayPal(Dto.PayPal creditCard) 
        => new(creditCard.UserName, creditCard.Password);

    public static implicit operator Dto.PayPal(PayPal creditCard) 
        => new(creditCard.UserName, creditCard.Password);
}