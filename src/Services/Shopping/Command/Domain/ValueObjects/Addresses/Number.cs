namespace Domain.ValueObjects.Addresses;

public record Number
{
    public string Value { get; }

    public Number(string number)
    {
        number = number.Trim();
        ArgumentOutOfRangeException.ThrowIfGreaterThan(number.Length, 10, nameof(number));

        Value = string.IsNullOrEmpty(number) ? "N/A" : number;
    }

    public static implicit operator Number(string number) => new(number);
    public static implicit operator string(Number number) => number.Value;
}