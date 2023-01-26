using System.Globalization;
using Contracts.DataTransferObjects;

namespace Domain.ValueObjects;

public record Money(decimal Amount, Currency Currency)
{
    public static Money operator +(Money money, Money other)
        => ApplyOperator(money, other, (first, second) => first.Amount + second.Amount);

    public static Money operator -(Money money, Money other)
        => ApplyOperator(money, other, (first, second) => first.Amount - second.Amount);

    public static Money operator *(Money money, Money other)
        => ApplyOperator(money, other, (first, second) => first.Amount * second.Amount);

    public static Money operator *(Money money, int integer)
        => money with { Amount = money.Amount * integer };

    public static Money operator /(Money money, Money other)
        => ApplyDivideByZeroOperator(money, other, (first, second) => first.Amount / second.Amount);

    public static Money operator %(Money money, Money other)
        => ApplyDivideByZeroOperator(money, other, (first, second) => first.Amount % second.Amount);

    public static bool operator >(Money money, Money other)
        => ApplyOperator(money, other, (first, second) => first.Amount > second.Amount);

    public static bool operator <(Money money, Money other)
        => ApplyOperator(money, other, (first, second) => first.Amount < second.Amount);

    public static bool operator ==(Money money, Dto.Money dto)
        => dto == (Dto.Money)money;

    public static bool operator !=(Money money, Dto.Money dto)
        => dto != (Dto.Money)money;

    public static implicit operator string(Money money)
        => $"{money.Currency.Symbol} {money.Amount.ToString("N", CultureInfo.GetCultureInfo(money.Currency.CultureInfo))}";

    public static implicit operator Money(Dto.Money dto)
    {
        Currency currency = dto.Currency;
        return new(decimal.Parse(dto.Amount, CultureInfo.GetCultureInfo(currency.CultureInfo)), currency);
    }

    public static implicit operator Dto.Money(Money money)
        => new(money.Amount.ToString("N", CultureInfo.GetCultureInfo(money.Currency.CultureInfo)), money.Currency);

    public override string ToString() => this;

    public static implicit operator decimal(Money money)
        => money.Amount;

    public static Money Zero(Currency currency)
        => new(decimal.Zero, currency);

    private static Money ApplyOperator(Money money, Money other, Func<Money, Money, decimal> operation)
    {
        EnsureCurrenciesAreEqual(money, other);
        return money with { Amount = operation(money, other) };
    }

    private static bool ApplyOperator(Money money, Money other, Func<Money, Money, bool> operation)
    {
        EnsureCurrenciesAreEqual(money, other);
        return operation(money, other);
    }

    private static Money ApplyDivideByZeroOperator(Money money, Money other, Func<Money, Money, decimal> operation)
    {
        if (other.Amount is 0) throw new DivideByZeroException();
        return ApplyOperator(money, other, operation);
    }

    private static void EnsureCurrenciesAreEqual(Money money, Money other)
    {
        if (money.Currency != other.Currency)
            throw new InvalidOperationException("Currencies must be the same");
    }
}