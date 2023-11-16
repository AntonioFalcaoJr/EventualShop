using Domain.Abstractions.Identities;

namespace Domain.Aggregates.ShoppingCarts;

public record CartId : GuidIdentifier
{
    public CartId() { }
    public CartId(string value) : base(value) { }

    public static CartId New => new();
    public static readonly CartId Undefined = new() { Value = Guid.Empty };

    public static explicit operator CartId(string value) => new(value);
    public override string ToString() => base.ToString();
}