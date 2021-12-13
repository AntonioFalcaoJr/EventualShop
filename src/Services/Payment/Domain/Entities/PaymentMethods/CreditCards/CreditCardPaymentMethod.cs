using System;

namespace Domain.Entities.PaymentMethods.CreditCards;

public class CreditCardPaymentMethod : PaymentMethod
{
    public CreditCardPaymentMethod(Guid id, decimal amount, DateOnly expiration, string holderName, string number, string securityNumber)
        : base(id, amount)
    {
        Expiration = expiration;
        HolderName = holderName;
        Number = number;
        SecurityNumber = securityNumber;
    }

    public DateOnly Expiration { get; }
    public string HolderName { get; }
    public string Number { get; }
    public string SecurityNumber { get; }

    protected override bool Validate()
        => OnValidate<CreditCardPaymentMethodValidator, CreditCardPaymentMethod>();
}