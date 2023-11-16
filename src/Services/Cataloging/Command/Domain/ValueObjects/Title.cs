namespace Domain.ValueObjects;

public record Title
{
    private readonly string _value;

    public Title(string productName)
    {
        productName = productName.Trim();
        ArgumentException.ThrowIfNullOrEmpty(productName);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(productName.Length, 50);

        _value = productName;
    }

    public static Title Undefined => new("Undefined");
    public static implicit operator Title(string title) => new(title);
    public static implicit operator string(Title title) => title._value;
    public override string ToString() => _value;
}