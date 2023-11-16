namespace Domain.ValueObjects.Addresses;

public record City
{
    public string Value { get; }

    public City(string city)
    {
        city = city.Trim();
        ArgumentException.ThrowIfNullOrEmpty(city, nameof(city));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(city.Length, 100, nameof(city));

        Value = city;
    }

    public static implicit operator City(string city) => new(city);
    public static implicit operator string(City city) => city.Value;
}