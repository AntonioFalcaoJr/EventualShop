namespace Domain.ValueObjects;

public class Unit
{
    private readonly string _value;

    public static Unit Unspecified => new("Undefined");

    private Unit(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Unit cannot be null or whitespace");

        _value = value;
    }

    public static implicit operator string(Unit unit) => unit._value;
    public static explicit operator Unit(string value) => new(value);
}