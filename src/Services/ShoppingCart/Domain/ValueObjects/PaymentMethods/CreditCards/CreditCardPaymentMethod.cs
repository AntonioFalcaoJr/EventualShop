using System;

namespace Domain.ValueObjects.PaymentMethods.CreditCards;

public record CreditCardPaymentMethod(decimal Amount, DateOnly Expiration, string HolderName, string Number, string SecurityNumber) : PaymentMethod(Amount)
{
    protected override bool Validate()
        => OnValidate<CreditCardPaymentMethodValidator, CreditCardPaymentMethod>();
}