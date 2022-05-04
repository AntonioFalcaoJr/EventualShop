using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.PaymentMethods.DebitCards;

public record DebitCard(decimal Amount, DateOnly Expiration, string HolderName, string Number, string SecurityNumber) : PaymentMethod<DebitCardValidator>(Amount)
{
    public static implicit operator DebitCard(Dto.DebitCard creditCard) 
        => new(creditCard.Amount, creditCard.Expiration, creditCard.Number, creditCard.HolderName, creditCard.SecurityNumber);

    // TODO - Review this Identifier default.
    public static implicit operator Dto.DebitCard(DebitCard creditCard) 
        => new(default, creditCard.Amount, creditCard.Expiration, creditCard.Number, creditCard.HolderName, creditCard.SecurityNumber);
}