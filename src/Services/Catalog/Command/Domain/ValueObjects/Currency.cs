namespace Domain.ValueObjects;

public record Currency(string IsoCode, string Symbol, int DecimalPlaces, string Name, string Country, string CultureInfo)
{
    public static readonly Currency BRL = new("BRL", "R$", 2, "Brazilian real", "Brazil", "pt-BR");
    public static readonly Currency CAD = new("CAD", "Can$", 2, "Canadian dollar", "Canada", "en-CA");
    public static readonly Currency USD = new("USD", "US$", 2, "United States dollar", "United States", "en-US");
    public static readonly Currency EUR = new("EUR", "€", 2, "Euro", "European Union", "en-EU");
    public static readonly Currency GBP = new("GBP", "£", 2, "British pound", "United Kingdom", "en-GB");
    public static readonly Currency JPY = new("JPY", "JP¥", 0, "Japanese yen", "Japan", "ja-JP");
    public static readonly Currency CHF = new("CHF", "CHF", 2, "Swiss franc", "Switzerland", "de-CH");
    public static readonly Currency AUD = new("AUD", "A$", 2, "Australian dollar", "Australia", "en-AU");
    public static readonly Currency CNY = new("CNY", "CNY", 2, "Chinese yuan", "China", "zh-CN");
    public static readonly Currency INR = new("INR", "INR", 2, "Indian rupee", "India", "hi-IN");
    public static readonly Currency MXN = new("MXN", "Mex$", 2, "Mexican peso", "Mexico", "es-MX");
    public static readonly Currency Undefined = new("Undefined", "Undefined", 0, "Undefined", "Undefined", "Undefined");

    public static implicit operator string(Currency currency)
        => currency.Symbol;

    public static implicit operator Currency(string currency)
        => currency switch
        {
            { } when BRL == currency => BRL,
            { } when CAD == currency => CAD,
            { } when USD == currency => USD,
            { } when EUR == currency => EUR,
            { } when GBP == currency => GBP,
            { } when JPY == currency => JPY,
            { } when CHF == currency => CHF,
            { } when AUD == currency => AUD,
            { } when CNY == currency => CNY,
            { } when INR == currency => INR,
            { } when MXN == currency => MXN,
            _ => Undefined
        };

    public static bool operator ==(Currency currency, string value)
        => string.Equals(currency.IsoCode, value.Trim(), StringComparison.OrdinalIgnoreCase) ||
           string.Equals(currency.Symbol, value.Trim(), StringComparison.OrdinalIgnoreCase);

    public static bool operator !=(Currency currency, string value)
        => string.Equals(currency.IsoCode, value.Trim(), StringComparison.OrdinalIgnoreCase) &&
           string.Equals(currency.Symbol, value.Trim(), StringComparison.OrdinalIgnoreCase) is false;
}