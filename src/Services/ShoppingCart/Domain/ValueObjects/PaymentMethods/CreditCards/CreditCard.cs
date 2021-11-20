using System;
using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.PaymentMethods.CreditCards;

public record CreditCard : ValueObject, IPaymentMethod
{
    public decimal Amount { get; init; }
    public DateOnly Expiration { get; init; }
    public string HolderName { get; init; }
    public string Number { get; init; }
    public string SecurityNumber { get; init; }

    protected override bool Validate()
        => OnValidate<CreditCardValidator, CreditCard>();
}