namespace Domain.ValueObjects;

public record Description
{
    private readonly string _value;

    private Description(string description)
    {
        description = description.Trim();
        ArgumentException.ThrowIfNullOrEmpty(description);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(description.Length, 500);

        _value = description;
    }

    public static Description Undefined => "Undefined";
    public static implicit operator string(Description description) => description._value;
    public static implicit operator Description(string description) => new(description);
    public override string ToString() => _value;
}