namespace Domain.ValueObjects;

public record Money(Amount Amount, Currency Currency)
{
    public static Money Zero(Currency currency) => new(Amount.Zero, currency);
    public static Money Zero(KeyValuePair<string, Currency> pair) => Zero(pair.Value);

    public static Money operator +(Money money, Money other)
        => ApplyOperator(money, other, (first, second) => first.Amount + second.Amount);

    public static Money operator -(Money money, Money other)
        => ApplyOperator(money, other, (first, second) => first.Amount - second.Amount);

    public static Money operator *(Money money, Money other)
        => ApplyOperator(money, other, (first, second) => first.Amount * second.Amount);

    public static Money operator *(Money money, Quantity quantity)
        => money with { Amount = new(money.Amount * quantity) };

    public static Money operator /(Money money, Money other)
        => ApplyDivideByZeroOperator(money, other, (first, second) => first.Amount / second.Amount);

    public static Money operator %(Money money, Money other)
        => ApplyDivideByZeroOperator(money, other, (first, second) => first.Amount % second.Amount);

    public static bool operator >(Money money, Money other)
        => ApplyOperator(money, other, (first, second) => first.Amount > second.Amount);

    public static bool operator <(Money money, Money other)
        => ApplyOperator(money, other, (first, second) => first.Amount < second.Amount);

    public static implicit operator string(Money money) => money.Amount;
    public override string ToString() => Amount.ToString("C", Currency.FormatInfo);

    private static Money ApplyOperator(Money money, Money other, Func<Money, Money, Amount> operation)
    {
        EnsureCurrenciesAreEqual(money, other);
        return money with { Amount = operation(money, other) };
    }

    private static bool ApplyOperator(Money money, Money other, Func<Money, Money, bool> operation)
    {
        EnsureCurrenciesAreEqual(money, other);
        return operation(money, other);
    }

    private static Money ApplyDivideByZeroOperator(Money money, Money other, Func<Money, Money, Amount> operation)
    {
        if (other.Amount == decimal.Zero) throw new DivideByZeroException();
        return ApplyOperator(money, other, operation);
    }

    private static void EnsureCurrenciesAreEqual(Money money, Money other)
    {
        if (money.Currency != other.Currency)
            throw new InvalidOperationException("Currencies must be the same");
    }
}