namespace Domain.ValueObjects;

public record Sku
{
    private readonly string _value;

    public Sku(Brand brand, Category category, Size size)
    {
        var brandPrefix = (string)brand;
        var categoryCode = (string)category;
        var sizeCode = (string)size;

        var sku = $"{brandPrefix}-{categoryCode}-{sizeCode}";

        // TODO: It have to moved to Cataloging context.
        //ArgumentOutOfRangeException.ThrowIfGreaterThan(sku.Length, 20);

        _value = sku;
    }

    private Sku(string sku)
    {
        sku = sku.Trim();
        ArgumentException.ThrowIfNullOrEmpty(sku);

        // TODO: It have to moved to Cataloging context.
        //ArgumentOutOfRangeException.ThrowIfGreaterThan(sku.Length, 20);

        _value = sku;
    }

    public static explicit operator Sku(string sku) => new(sku);
    public static implicit operator string(Sku sku) => sku._value;
    public static Sku Undefined => new("Undefined");

    public override string ToString() => _value;
}

public record Size(string Code, string Description)
{
    public static readonly Size ExtraSmall = new("XS", "Extra Small");
    public static readonly Size Small = new("S", "Small");
    public static readonly Size Medium = new("M", "Medium");
    public static readonly Size Large = new("L", "Large");
    public static readonly Size ExtraLarge = new("XL", "Extra Large");
    public static readonly Size ExtraExtraLarge = new("XXL", "Extra Extra Large");

    public static Dictionary<string, Size> All { get; } = new()
    {
        { ExtraSmall.Code, ExtraSmall },
        { Small.Code, Small },
        { Medium.Code, Medium },
        { Large.Code, Large },
        { ExtraLarge.Code, ExtraLarge },
        { ExtraExtraLarge.Code, ExtraExtraLarge }
    };

    public static explicit operator Size(string code)
        => All.TryGetValue(code, out var size) ? size
            : throw new ArgumentException($"Size {size} is not supported.");

    public static implicit operator string(Size size) => size.Code;
    public override string ToString() => Code;
}