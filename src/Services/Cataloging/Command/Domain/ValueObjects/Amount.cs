using System.Globalization;

namespace Domain.ValueObjects;

public record Amount
{
    private readonly decimal _value;

    private Amount(string amount)
    {
        _value = decimal.Parse(amount, NumberStyles.Number, CultureInfo.InvariantCulture);
    }

    public Amount(decimal amount)
    {
        _value = amount;
    }

    public static Amount Zero => new(decimal.Zero);

    public static explicit operator Amount(decimal amount) => new(amount);
    public static implicit operator decimal(Amount amount) => amount._value;
    public static explicit operator Amount(string amount) => new(amount);
    public static implicit operator string(Amount amount) => amount.ToString();

    public static Amount operator +(Amount amount, Amount other) => new(amount._value + other._value);
    public static Amount operator -(Amount amount, Amount other) => new(amount._value - other._value);
    public static Amount operator *(Amount amount, Amount other) => new(amount._value * other._value);
    public static Amount operator /(Amount amount, Amount other) => new(amount._value / other._value);
    public static Amount operator %(Amount amount, Amount other) => new(amount._value % other._value);
    public static bool operator >(Amount amount, Amount other) => amount._value > other._value;
    public static bool operator <(Amount amount, Amount other) => amount._value < other._value;

    public string ToString(string? format, IFormatProvider? provider) => _value.ToString(format, provider);
    public override string ToString() => _value.ToString("N", NumberFormatInfo.InvariantInfo);
}