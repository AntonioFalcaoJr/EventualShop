namespace Domain.ValueObjects.Addresses;

public record Country
{
    public string Value { get; }

    public Country(string country)
    {
        country = country.Trim();
        ArgumentException.ThrowIfNullOrEmpty(country, nameof(country));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(country.Length, 100, nameof(country));

        Value = country;
    }

    public static implicit operator Country(string country) => new(country);
    public static implicit operator string(Country country) => country.Value;
}