namespace Domain.ValueObjects;

public readonly record struct Money(Currency Currency, decimal Value)
{
    public static Money operator +(Money money, Money other)
        => money with { Value = money.Value + other.Value };

    public static Money operator -(Money money, Money other)
        => money with { Value = money.Value - other.Value };

    public static Money operator *(Money money, Money other)
        => money with { Value = money.Value * other.Value };

    public static Money operator *(Money money, int integer)
        => money with { Value = money.Value * integer };

    public static Money operator /(Money money, Money other)
    {
        if (other.Value is 0) throw new DivideByZeroException();
        return money with { Value = money.Value / other.Value };
    }

    public static Money operator %(Money money, Money other)
    {
        if (other.Value is 0) throw new DivideByZeroException();
        return money with { Value = money.Value % other.Value };
    }

    public static bool operator >(Money money, Money other)
        => money.Value > other.Value;

    public static bool operator <(Money money, Money other)
        => money.Value < other.Value;

    public static implicit operator string(Money money)
        => $"{money.Currency.Symbol} {money.Value}";

    public static implicit operator Money(string money)
    {
        var parts = money.Split(' ');
        return new(parts[1], decimal.Parse(parts[0]));
    }

    public static implicit operator decimal(Money money)
        => money.Value;

    public static Money Zero(Currency currency)
        => new(currency, 0);
}