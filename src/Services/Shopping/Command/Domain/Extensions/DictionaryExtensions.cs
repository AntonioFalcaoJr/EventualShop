using Domain.Entities.CartItems;
using Domain.ValueObjects;

namespace Domain.Extensions;

public static class DictionaryExtensions
{
    public static IDictionary<string, string> AsString(this IDictionary<Currency, Money> totals)
        => totals.ToDictionary(money => (string)money.Key, money => (string)money.Value);

    public static IDictionary<Currency, Money> ToMoneyDictionary(this IDictionary<string, string> totals)
        => totals.ToDictionary(money => (Currency)money.Key, money => new Money((Amount)money.Value, (Currency)money.Key));

    public static IDictionary<string, string> AsString(this IDictionary<Currency, Price> prices)
        => prices.ToDictionary(price => (string)price.Key, price => (string)price.Value);

    public static IDictionary<Currency, Price> ToPriceDictionary(this IDictionary<string, string> prices)
        => prices.ToDictionary(price => (Currency)price.Key, price => new Price((Amount)price.Value, (Currency)price.Key));

    public static IDictionary<string, string> Project(
        this IDictionary<Currency, Money> totals, IDictionary<Currency, Price> prices, Quantity quantity)
        => totals.ToDictionary(total => total.Key, total => prices[total.Key] * quantity as Money).AsString();

    public static IDictionary<string, string> Project(this IDictionary<Currency, Money> totals, CartItem cartItem)
        => totals.ToDictionary(total => total.Key, total => total.Value + cartItem.Prices[total.Key] * cartItem.Quantity).AsString();
}