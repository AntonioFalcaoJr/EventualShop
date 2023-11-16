using Domain.Abstractions.Identities;

namespace Domain.Aggregates.Checkouts;

public record CheckoutId : GuidIdentifier
{
    public CheckoutId() { }
    public CheckoutId(string value) : base(value) { }
    
    public static CheckoutId New => new();
    public static readonly CheckoutId Undefined = new() { Value = Guid.Empty };
    
    public static explicit operator CheckoutId(string value) => new(value);
    public override string ToString() => base.ToString();
}