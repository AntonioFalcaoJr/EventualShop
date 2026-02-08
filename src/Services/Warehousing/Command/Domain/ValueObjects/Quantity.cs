namespace Domain.ValueObjects;

public record Quantity
{
    private readonly ushort _value;

    public Quantity(ushort quantity)
    {
        _value = quantity;
    }

    private Quantity(string quantity)
    {
        quantity = quantity.Trim();

        if (ushort.TryParse(quantity, out var parsedQuantity) is false)
            throw new ArgumentException("Quantity must be a valid number");

        ArgumentOutOfRangeException.ThrowIfZero(parsedQuantity);

        _value = parsedQuantity;
    }

    private Quantity(int quantity)
    {
        if (quantity is < ushort.MinValue or > ushort.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be a valid number");

        _value = (ushort)quantity;
    }

    public static Quantity Zero { get; } = new(ushort.MinValue);
    public static Quantity Max { get; } = new(ushort.MaxValue);

    public static Quantity Number(ushort quantity) => new(quantity);
    public static explicit operator Quantity(ushort quantity) => new(quantity);
    public static implicit operator ushort(Quantity quantity) => quantity._value;
    public static explicit operator Quantity(string quantity) => new(quantity);
    public static implicit operator string(Quantity quantity) => quantity._value.ToString();

    public static Quantity operator +(Quantity left, Quantity right) => new(left._value + right._value);
    public static Quantity operator -(Quantity left, Quantity right) => new(left._value - right._value);
    public static Quantity operator *(Quantity left, Quantity right) => new(left._value * right._value);
    public static bool operator <(Quantity left, Quantity right) => left._value < right._value;
    public static bool operator >(Quantity left, Quantity right) => left._value > right._value;
    public static bool operator <=(Quantity left, Quantity right) => left._value <= right._value;
    public static bool operator >=(Quantity left, Quantity right) => left._value >= right._value;

    public override string ToString() => _value.ToString();
}