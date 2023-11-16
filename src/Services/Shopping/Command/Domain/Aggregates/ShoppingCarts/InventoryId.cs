using Domain.Abstractions.Identities;

namespace Domain.Aggregates.ShoppingCarts;

public record InventoryId : GuidIdentifier
{
    public InventoryId() { }
    public InventoryId(string value) : base(value) { }

    public static InventoryId New => new();
    public static readonly InventoryId Undefined = new() { Value = Guid.Empty };

    public static implicit operator InventoryId(string value) => new(value);
    public override string ToString() => base.ToString();
}