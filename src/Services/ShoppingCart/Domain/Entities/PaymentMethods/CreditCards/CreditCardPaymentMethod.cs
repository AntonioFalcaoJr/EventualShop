using System;

namespace Domain.Entities.PaymentMethods.CreditCards;

public class CreditCardPaymentMethod : PaymentMethod
{
    public DateOnly Expiration { get; init; }
    public string HolderName { get; init; }
    public string Number { get; init; }
    public string SecurityNumber { get; init; }

    protected override bool Validate()
        => OnValidate<CreditCardPaymentMethodValidator, CreditCardPaymentMethod>();
}