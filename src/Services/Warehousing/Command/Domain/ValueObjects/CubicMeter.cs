namespace Domain.ValueObjects;

public class CubicMeter
{
    private decimal _value;
    public static CubicMeter Zero => new(0);

    public CubicMeter(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Cubic meter value cannot be negative");

        _value = value;
    }
}