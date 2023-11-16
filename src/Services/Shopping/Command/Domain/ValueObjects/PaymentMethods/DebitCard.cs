using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.PaymentMethods;

public record DebitCard(
    ExpirationDate ExpirationDate,
    CreditCardNumber Number,
    CardHolderName HolderName,
    Cvv Cvv) : IPaymentMethod
{
    public static implicit operator DebitCard(Dto.DebitCard card)
        => new(card.ExpirationDate, card.Number, card.HolderName, card.SecurityCode);

    public static implicit operator Dto.DebitCard(DebitCard card)
        => new(card.ExpirationDate, card.Number, card.HolderName, card.Cvv);
}