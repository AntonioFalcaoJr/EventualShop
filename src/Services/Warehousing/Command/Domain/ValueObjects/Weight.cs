using System.Globalization;

namespace Domain.ValueObjects;

public record Weight
{
    private readonly decimal _value;

    private Weight(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Weight cannot be negative");

        _value = value;
    }

    private Weight(string weight)
    {
        if (decimal.TryParse(weight, out var value) is false)
            throw new ArgumentException("Weight is not a valid number");

        if (value < 0)
            throw new ArgumentException("Weight cannot be negative");

        _value = value;
    }

    public static Weight Zero => new(0);

    public static explicit operator Weight(string weight) => new(weight);
    public static implicit operator string(Weight weight) => weight.ToString();

    public override string ToString() => _value.ToString(CultureInfo.InvariantCulture);
}