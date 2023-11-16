namespace Domain.ValueObjects.Addresses;

public record Complement
{
    public string Value { get; }

    public Complement(string complement)
    {
        complement = complement.Trim();
        ArgumentOutOfRangeException.ThrowIfGreaterThan(complement.Length, 10, nameof(complement));

        Value = string.IsNullOrEmpty(complement) ? "N/A" : complement;
    }

    public static implicit operator Complement(string complement) => new(complement);
    public static implicit operator string(Complement complement) => complement.Value;
}