using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.PaymentOptions.DebitCards;

public record DebitCard(string Expiration, string HolderName, string Number, ushort SecurityNumber) : IPaymentOption
{
    public static implicit operator DebitCard(Dto.DebitCard creditCard)
        => new(creditCard.ExpirationDate, creditCard.Number, creditCard.HolderName, creditCard.SecurityCode);

    public static implicit operator Dto.DebitCard(DebitCard creditCard)
        => new(creditCard.Expiration, creditCard.Number, creditCard.HolderName, creditCard.SecurityNumber);
}