using Domain.Enumerations;

namespace Domain.ValueObjects;

public readonly record struct Money
{
     private readonly decimal _amount;
     public Currency Currency { get; }

     public Money(Currency currency,  decimal amount)
     {
         _amount = amount;
         Currency = currency;
     }

     public decimal Value => _amount;

    public static Money operator +(Money money1, Money money2)
    {
        return new Money(money1.Currency, money1.Value / money2.Value);
    }
    
    public static Money operator -(Money money1, Money money2)
    {
        return new Money(money1.Currency, money1.Value / money2.Value);
    }
    
    public static Money operator *(Money money1, Money money2)
    {
        return new Money(money1.Currency, money1.Value / money2.Value);
    }
    
    public static Money operator /(Money money1, Money money2)
    {
        if (money2.Value == 0) 
            throw new DivideByZeroException();

        return new Money(money1.Currency, money1.Value / money2.Value);
    }
    
    public static implicit operator Money((decimal amount, string symbol) money) 
        => new (money.symbol, money.amount);
}

