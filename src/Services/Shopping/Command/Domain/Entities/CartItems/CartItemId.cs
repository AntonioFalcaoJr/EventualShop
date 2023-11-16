using Domain.Abstractions.Identities;

namespace Domain.Entities.CartItems;

public record CartItemId : GuidIdentifier
{
    // TODO: CartItemId should be a composite of CartId and ProductId
    // public CartId CartId { get; init; } = CartId.Undefined;
    // public ProductId ProductId { get; init; } = ProductId.Undefined;

    public CartItemId() { }
    public CartItemId(string value) : base(value) { }

    public static CartItemId New => new();
    public static readonly CartItemId Undefined = new() { Value = Guid.Empty };

    public static implicit operator CartItemId(string value) => new(value);
    public override string ToString() => base.ToString();
}