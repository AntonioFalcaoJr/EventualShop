namespace Domain.ValueObjects;

public record struct Currency(string IsoCode, string Symbol, int DecimalPlaces, string Name, string Country)
{
    public static Currency CAD => new("CAD", "$", 2, "Canadian dollar", "Canada");
    public static Currency USD => new("USD", "$", 2, "United States dollar", "United States");
    public static Currency EUR => new("EUR", "€", 2, "Euro", "European Union");
    public static Currency GBP => new("GBP", "£", 2, "British pound", "United Kingdom");
    public static Currency JPY => new("JPY", "¥", 0, "Japanese yen", "Japan");
    public static Currency CHF => new("CHF", "Fr", 2, "Swiss franc", "Switzerland");
    public static Currency AUD => new("AUD", "$", 2, "Australian dollar", "Australia");
    public static Currency NZD => new("NZD", "$", 2, "New Zealand dollar", "New Zealand");
    public static Currency SEK => new("SEK", "kr", 2, "Swedish krona", "Sweden");
    public static Currency NOK => new("NOK", "kr", 2, "Norwegian krone", "Norway");

    public static implicit operator Currency(string isoCode)
        => isoCode switch
        {
            "CAD" => CAD,
            "USD" => USD,
            "EUR" => EUR,
            "GBP" => GBP,
            "JPY" => JPY,
            "CHF" => CHF,
            "AUD" => AUD,
            "NZD" => NZD,
            "SEK" => SEK,
            "NOK" => NOK,
            _ => throw new ArgumentException($"Unknown currency: {isoCode}")
        };
}