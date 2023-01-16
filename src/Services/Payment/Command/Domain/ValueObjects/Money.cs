using Domain.Enumerations;

namespace Domain.ValueObjects;

public readonly record struct Money
{
     private readonly decimal _value;
     public Currency Currency { get; }

     public Money(Currency currency,  decimal value)
     {
         _value = value;
         Currency = currency;
     }

     public decimal Value => _value;

    public static Money operator +(Money amount1, Money amount2)
    {
        return new Money(amount1.Currency, amount1.Value / amount2.Value);
    }
    
    public static Money operator -(Money amount1, Money amount2)
    {
        return new Money(amount1.Currency, amount1.Value / amount2.Value);
    }
    
    public static Money operator *(Money amount1, Money amount2)
    {
        return new Money(amount1.Currency, amount1.Value / amount2.Value);
    }
    
    public static Money operator /(Money amount1, Money amount2)
    {
        if (amount2.Value == 0) 
            throw new DivideByZeroException();

        return new Money(amount1.Currency, amount1.Value / amount2.Value);
    }
    
    public static implicit operator Money((decimal value, string symbol) amount) 
        => new (amount.symbol, amount.value);
}

