// ReSharper disable InconsistentNaming

namespace Domain.ValueObjects;

public record Currency(string IsoCode, string Symbol, int DecimalPlaces, string Name, string Country, string CultureInfo)
{
    public static Currency BRL => new("BRL", "R$", 2, "Brazilian Real", "Brazil", "pt-BR");
    public static Currency CAD => new("CAD", "Can$", 2, "Canadian dollar", "Canada", "en-CA");
    public static Currency USD => new("USD", "US$", 2, "United States dollar", "United States", "en-US");
    public static Currency EUR => new("EUR", "€", 2, "Euro", "European Union", "en-EU");
    public static Currency GBP => new("GBP", "£", 2, "British pound", "United Kingdom", "en-GB");
    public static Currency JPY => new("JPY", "JP¥", 0, "Japanese yen", "Japan", "ja-JP");
    public static Currency CHF => new("CHF", "CHF", 2, "Swiss franc", "Switzerland", "de-CH");
    public static Currency AUD => new("AUD", "A$", 2, "Australian dollar", "Australia", "en-AU");
    public static Currency CNY => new("CNY", "CNY", 2, "Chinese yuan", "China", "zh-CN");
    public static Currency INR => new("INR", "INR", 2, "Indian rupee", "India", "hi-IN");
    public static Currency MXN => new("MXN", "Mex$", 2, "Mexican peso", "Mexico", "es-MX");
    public static Currency Unknown => new("Unknown", "Unknown", 0, "Unknown", "Unknown", "Unknown");

    public static implicit operator Currency(string currency)
        => currency switch
        {
            "BRL" or "R$" => BRL,
            "CAD" or "Can$" => CAD,
            "USD" or "US$" => USD,
            "EUR" or "€" => EUR,
            "GBP" or "£" => GBP,
            "JPY" or "JP¥" => JPY,
            "CHF" => CHF,
            "AUD" or "A$" => AUD,
            "CNY" => CNY,
            "INR" => INR,
            "MXN" or "Mex$" => MXN,
            _ => Unknown
        };

    public static implicit operator string(Currency currency)
        => currency.Symbol;
}