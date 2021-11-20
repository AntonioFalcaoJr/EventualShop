using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.PaymentMethods.PayPals;

public record PayPal : ValueObject, IPaymentMethod
{
    public decimal Amount { get; init; }

    protected override bool Validate() => throw new System.NotImplementedException();
}