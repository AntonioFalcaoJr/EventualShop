using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.PaymentMethods;

public record CreditCard(ExpirationDate ExpirationDate, CreditCardNumber Number, CardHolderName HolderName, Cvv Cvv) : IPaymentMethod
{
    public static implicit operator CreditCard(Dto.CreditCard card)
        => new(card.ExpirationDate, card.Number, card.HolderName, card.Cvv);

    public static implicit operator Dto.CreditCard(CreditCard card)
        => new(card.ExpirationDate, card.Number, card.HolderName, card.Cvv);
}