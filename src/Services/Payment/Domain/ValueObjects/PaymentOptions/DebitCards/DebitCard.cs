using Contracts.DataTransferObjects;
using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.PaymentOptions.DebitCards;

public record DebitCard(DateOnly Expiration, string HolderName, string Number, ushort SecurityNumber) : ValueObject<DebitCardValidator>, IPaymentOption
{
    public static implicit operator DebitCard(Dto.DebitCard creditCard)
        => new(creditCard.ExpirationDate, creditCard.Number, creditCard.HolderName, creditCard.SecurityCode);

    public static implicit operator Dto.DebitCard(DebitCard creditCard)
        => new(creditCard.Expiration, creditCard.Number, creditCard.HolderName, creditCard.SecurityNumber);
}