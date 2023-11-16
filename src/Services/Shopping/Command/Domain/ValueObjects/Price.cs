namespace Domain.ValueObjects;

public record Price : Money
{
    public Price(Amount amount, Currency currency) : base(amount, currency)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual<decimal>(
            amount, Amount.Zero, "Amount must be positive");
    }

    public static implicit operator string(Price price) => price.Amount;
    public static implicit operator KeyValuePair<string, string>(Price price) => new(price.Currency, price.Amount);
    public static Price operator *(Price price, Quantity quantity) => price with { Amount = new(price.Amount * quantity) };
    public override string ToString() => base.ToString();
}