using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.PaymentMethods.Invoices;

public record Invoice : ValueObject, IPaymentMethod
{
    public decimal Amount { get; init; }
    
    protected override bool Validate() => throw new System.NotImplementedException();
}