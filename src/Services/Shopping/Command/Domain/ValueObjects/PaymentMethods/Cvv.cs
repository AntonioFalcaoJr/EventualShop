using static Domain.Exceptions;

namespace Domain.ValueObjects.PaymentMethods;

public record Cvv
{
    private readonly string _value;

    public Cvv(string value)
    {
        InvalidSecurityCode.ThrowIf(
            value.Length is not 3 ||
            value.All(char.IsDigit) is false);

        _value = value;
    }

    public static implicit operator Cvv(string cvv) => new(cvv);
    public static implicit operator string(Cvv cvv) => cvv._value;

    public override string ToString() => _value;
}