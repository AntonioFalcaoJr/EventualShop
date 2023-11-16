using Domain.Abstractions.Identities;

namespace Domain.Aggregates.ShoppingCarts;

public record CustomerId : GuidIdentifier
{
    public CustomerId() { }
    public CustomerId(string value) : base(value) { }

    public static CustomerId New => new();
    public static readonly CustomerId Undefined = new() { Value = Guid.Empty };

    public static explicit operator CustomerId(string value) => new(value);
    public override string ToString() => base.ToString();
}