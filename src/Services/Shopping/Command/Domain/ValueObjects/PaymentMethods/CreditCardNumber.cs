using static Domain.Exceptions;

namespace Domain.ValueObjects.PaymentMethods;

public record CreditCardNumber
{
    private readonly string _value;

    public CreditCardNumber(string value)
    {
        value = value.Replace(" ", "");

        InvalidCardNumber.ThrowIf(
            string.IsNullOrEmpty(value) ||
            value.Length is not 16 ||
            value.All(char.IsDigit) is false);

        _value = value;
    }

    public static implicit operator CreditCardNumber(string number) => new(number);
    public static implicit operator string(CreditCardNumber number) => number._value;

    public override string ToString() => _value.Insert(4, " ").Insert(9, " ").Insert(14, " ");
}