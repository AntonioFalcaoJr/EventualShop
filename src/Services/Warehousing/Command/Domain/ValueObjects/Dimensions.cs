using System.Globalization;

namespace Domain.ValueObjects;

public record Dimensions(Length Length, Width Width, Height Height)
{
    public static Dimensions Zero => new(Length.Zero, Width.Zero, Height.Zero);
    public CubicMeter M3 => new(Length * Width * Height);

    public static implicit operator Dimensions((Length Length, Width Width, Height Height) dimensions) 
        => new(dimensions.Length, dimensions.Width, dimensions.Height);
    public override string ToString() => $"{Length} x {Width} x {Height}";
}

public record Dimension
{
    private readonly decimal _value;

    protected Dimension(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Dimension cannot be negative");

        _value = value;
    }

    protected Dimension(string value)
    {
        if (!decimal.TryParse(value, out var result))
            throw new ArgumentException("Dimension cannot be parsed");

        if (result < 0)
            throw new ArgumentException("Dimension cannot be negative");

        _value = result;
    }

    public static implicit operator Dimension(decimal value) => new(value);
    public static implicit operator string(Dimension dimension) => dimension.ToString();
    public static explicit operator Dimension(string value) => new(value);

    public static decimal operator *(Dimension dimension, Dimension other) => dimension._value * other._value;

    public override string ToString() => _value.ToString(CultureInfo.InvariantCulture);
}

public record Length : Dimension
{
    public static Length Zero => new(0);

    private Length(decimal value) : base(value) { }
    private Length(string value) : base(value) { }
}

public record Width : Dimension
{
    public static Width Zero => new(0);

    private Width(decimal value) : base(value) { }
    private Width(string value) : base(value) { }
}

public record Height : Dimension
{
    public static Height Zero => new(0);

    private Height(decimal value) : base(value) { }
    private Height(string value) : base(value) { }
}