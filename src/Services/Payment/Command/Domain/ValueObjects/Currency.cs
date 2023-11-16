using System.Globalization;

namespace Domain.ValueObjects;

public record Currency(string IsoCode, string Name, string Country, NumberFormatInfo FormatInfo)
{
    public static readonly Currency BRL = new("BRL", "Brazilian real", "Brazil", new CultureInfo("pt-BR").NumberFormat);
    public static readonly Currency CAD = new("CAD", "Canadian dollar", "Canada", new CultureInfo("en-CA").NumberFormat);
    public static readonly Currency USD = new("USD", "United States dollar", "United States", new CultureInfo("en-US").NumberFormat);
    public static readonly Currency EUR = new("EUR", "Euro", "European Union", new CultureInfo("fr-FR").NumberFormat);
    public static readonly Currency GBP = new("GBP", "British pound", "United Kingdom", new CultureInfo("en-GB").NumberFormat);
    public static readonly Currency JPY = new("JPY", "Japanese yen", "Japan", new CultureInfo("ja-JP").NumberFormat);
    public static readonly Currency CHF = new("CHF", "Swiss franc", "Switzerland", new CultureInfo("de-CH").NumberFormat);
    public static readonly Currency AUD = new("AUD", "Australian dollar", "Australia", new CultureInfo("en-AU").NumberFormat);
    public static readonly Currency CNY = new("CNY", "Chinese yuan", "China", new CultureInfo("zh-CN").NumberFormat);
    public static readonly Currency INR = new("INR", "Indian rupee", "India", new CultureInfo("hi-IN").NumberFormat);
    public static readonly Currency MXN = new("MXN", "Mexican peso", "Mexico", new CultureInfo("es-MX").NumberFormat);
    public static readonly Currency Undefined = new("Undefined", "Undefined", "Undefined", NumberFormatInfo.InvariantInfo);

    public Currency(string IsoCode) : this(IsoCode, All[IsoCode].Name, All[IsoCode].Country, All[IsoCode].FormatInfo) { }

    public static Dictionary<string, Currency> All { get; } = new()
    {
        { BRL.IsoCode, BRL }, { CAD.IsoCode, CAD }, { USD.IsoCode, USD },
        { EUR.IsoCode, EUR }, { GBP.IsoCode, GBP }, { JPY.IsoCode, JPY },
        { CHF.IsoCode, CHF }, { AUD.IsoCode, AUD }, { CNY.IsoCode, CNY },
        { INR.IsoCode, INR }, { MXN.IsoCode, MXN }
    };

    public static explicit operator Currency(string isoCode)
        => All.TryGetValue(isoCode, out var currency) ? currency
            : throw new ArgumentException($"Currency {isoCode} is not supported.");

    public static implicit operator string(Currency currency) => currency.IsoCode;

    public static bool operator ==(Currency currency, string value)
        => string.Equals(currency.IsoCode, value.Trim(), StringComparison.OrdinalIgnoreCase) ||
           string.Equals(currency.FormatInfo.CurrencySymbol, value.Trim(), StringComparison.OrdinalIgnoreCase);

    public static bool operator !=(Currency currency, string value)
        => string.Equals(currency.IsoCode, value.Trim(), StringComparison.OrdinalIgnoreCase) &&
           string.Equals(currency.FormatInfo.CurrencySymbol, value.Trim(), StringComparison.OrdinalIgnoreCase) is false;

    public override string ToString() => IsoCode;
}