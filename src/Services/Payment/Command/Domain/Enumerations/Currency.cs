using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace Domain.Enumerations;

public class Currency : SmartEnum<Currency>
{
    [JsonConstructor]
    private Currency(string abbreviation, string symbol, int value)
        : base(symbol, value)
    {
        Abbreviation = abbreviation;
    }

    public static readonly Currency AUD = new AudCurrency();
    public static readonly Currency USD = new UsdCurrency();
    public static readonly Currency BRL = new BrlCurrency();
    public static readonly Currency JPY = new JpyCurrency();
    public static readonly Currency GBP = new GbpCurrency();
    public static readonly Currency CNY = new CnyCurrency();

    public static implicit operator Currency(string name)
        => FromName(name);

    public static implicit operator Currency(int value)
        => FromValue(value);

    public static implicit operator string(Currency status)
        => status.Name;

    public static implicit operator int(Currency status)
        => status.Value;
    
    public string Abbreviation { get; }


    public class AudCurrency : Currency
    {
        public AudCurrency() : base(nameof(AUD),"AU$",  1) { }
    }
    
    public class UsdCurrency : Currency
    {
        public UsdCurrency() : base(nameof(USD), "$",2) { }
    }
    
    public class BrlCurrency : Currency
    {
        public BrlCurrency() : base(nameof(BRL), "R$" , 3) { }
    }
    
    public class JpyCurrency : Currency
    {
        public JpyCurrency() : base(nameof(JPY),"JP¥", 4) { }
    }
    
    public class GbpCurrency : Currency
    {
        public GbpCurrency() : base(nameof(GBP), "£", 6) { }
    }
    
    public class CnyCurrency : Currency
    {
        public CnyCurrency() : base(nameof(CNY), "CN¥",7) { }
    }
}