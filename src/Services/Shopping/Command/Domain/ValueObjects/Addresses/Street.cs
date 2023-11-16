namespace Domain.ValueObjects.Addresses;

public record Street
{
    public string Value { get; }

    public Street(string street)
    {
        street = street.Trim();
        ArgumentException.ThrowIfNullOrEmpty(street, nameof(street));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(street.Length, 100, nameof(street));

        Value = street;
    }

    public static implicit operator Street(string street) => new(street);
    public static implicit operator string(Street street) => street.Value;
}