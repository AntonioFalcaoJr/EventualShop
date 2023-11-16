using static Domain.Exceptions;

namespace Domain.ValueObjects.PaymentMethods;

public record CardHolderName
{
    private readonly string _value;

    public CardHolderName(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        InvalidCardholderName.ThrowIf(
            value.Length is < 2 or > 50 || value.All(char.IsLetter) is false);

        _value = value.Trim();
    }

    public static implicit operator string(CardHolderName name) => name._value;
    public static implicit operator CardHolderName(string name) => new(name);
}