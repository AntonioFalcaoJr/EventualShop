namespace Domain.ValueObjects.Addresses;

public record ZipCode
{
    public string Value { get; }

    public ZipCode(string zipCode)
    {
        zipCode = zipCode.Trim();
        ArgumentException.ThrowIfNullOrEmpty(zipCode, nameof(zipCode));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(zipCode.Length, 10, nameof(zipCode));

        Value = zipCode;
    }

    public static implicit operator ZipCode(string zipCode) => new(zipCode);
    public static implicit operator string(ZipCode zipCode) => zipCode.Value;
}