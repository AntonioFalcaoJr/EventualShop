using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.PaymentMethods.CreditCards;

public record CreditCard(decimal Amount, DateOnly Expiration, string Number, string HolderName, string SecurityNumber) : PaymentMethod<CreditCardValidator>(Amount)
{
    public static implicit operator CreditCard(Dto.CreditCard creditCard) 
        => new(creditCard.Amount, creditCard.Expiration, creditCard.Number, creditCard.HolderName, creditCard.SecurityNumber);

    // TODO - Review this Identifier default.
    public static implicit operator Dto.CreditCard(CreditCard creditCard) 
        => new(default,creditCard.Amount, creditCard.Expiration, creditCard.Number, creditCard.HolderName, creditCard.SecurityNumber);
}