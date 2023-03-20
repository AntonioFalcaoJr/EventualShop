using Domain.Abstractions;

namespace Domain.Aggregates;

public record ShoppingCartId : Identifier<Guid>
{
    public ShoppingCartId()
        : base(Guid.NewGuid()) { }

    private ShoppingCartId(string value)
        : base(value) { }

    public static ShoppingCartId New()
        => new();

    public static implicit operator ShoppingCartId(string id)
        => new(id);
}