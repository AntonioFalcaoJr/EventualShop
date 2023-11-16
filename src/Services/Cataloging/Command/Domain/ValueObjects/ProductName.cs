namespace Domain.ValueObjects;

public record ProductName
{
    private readonly string _value;

    public ProductName(string productName)
    {
        productName = productName.Trim();
        ArgumentException.ThrowIfNullOrEmpty(productName);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(productName.Length, 50);

        _value = productName;
    }

    public static explicit operator ProductName(string productName) => new(productName);
    public static implicit operator string(ProductName productName) => productName._value;
    public static ProductName Undefined => new("Undefined");

    public override string ToString() => _value;
}