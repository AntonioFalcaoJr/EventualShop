using System;
using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.CreditCards;

public record CreditCard : ValueObject
{
    public DateOnly Expiration { get; init; }
    public string HolderName { get; init; }
    public string Number { get; init; }
    public string SecurityNumber { get; init; }

    protected override bool Validate()
        => OnValidate<CreditCardValidator, CreditCard>();
}