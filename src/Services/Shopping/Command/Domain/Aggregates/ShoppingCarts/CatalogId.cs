using Domain.Abstractions.Identities;

namespace Domain.Aggregates.ShoppingCarts;

public record CatalogId : GuidIdentifier
{
    public CatalogId() { }
    public CatalogId(string value) : base(value) { }

    public static CatalogId New => new();
    public static readonly CatalogId Undefined = new() { Value = Guid.Empty };

    public static explicit operator CatalogId(string value) => new(value);
    public override string ToString() => base.ToString();
}