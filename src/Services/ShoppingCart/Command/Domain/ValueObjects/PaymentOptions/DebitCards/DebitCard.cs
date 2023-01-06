using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.PaymentOptions.DebitCards;

public record DebitCard(DateOnly Expiration, string HolderName, string Number, ushort SecurityNumber) : IPaymentOption
{
    public static implicit operator DebitCard(Dto.DebitCard creditCard)
        => new(creditCard.Expiration, creditCard.Number, creditCard.HolderName, creditCard.SecurityNumber);

    public static implicit operator Dto.DebitCard(DebitCard creditCard)
        => new(creditCard.Expiration, creditCard.Number, creditCard.HolderName, creditCard.SecurityNumber);
}