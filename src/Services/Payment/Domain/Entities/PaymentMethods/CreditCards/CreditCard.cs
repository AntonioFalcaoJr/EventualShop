using Contracts.DataTransferObjects;

namespace Domain.Entities.PaymentMethods.CreditCards;

public class CreditCard : PaymentMethod<CreditCardPaymentMethodValidator>
{
    public CreditCard(Guid? id, decimal amount, DateOnly expiration, string holderName, string number, string securityNumber)
        : base(id, amount)
    {
        Expiration = expiration;
        HolderName = holderName;
        Number = number;
        SecurityNumber = securityNumber;
    }

    public DateOnly Expiration { get; }
    public string HolderName { get; }
    public string Number { get; }
    public string SecurityNumber { get; }


    public static implicit operator CreditCard(Dto.CreditCard card) 
        => new(card.Id, card.Amount, card.Expiration, card.Number, card.HolderName, card.SecurityNumber);

    public static implicit operator Dto.CreditCard(CreditCard card)
        => new(card.Id, card.Amount, card.Expiration, card.Number, card.HolderName, card.SecurityNumber);
}