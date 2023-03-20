using Domain.Abstractions;

namespace Domain.Entities.CartItems;

public record CartItemId : Identifier<Guid>
{
    public CartItemId()
        : base(Guid.NewGuid()) { }

    private CartItemId(Guid value)
        : base(value) { }

    private CartItemId(string value)
        : base(value) { }

    public static CartItemId New()
        => new();

    public static implicit operator CartItemId(string value)
        => new(value);
}