using Contracts.DataTransferObjects;

namespace Domain.Entities.PaymentMethods.DebitCards;

public class DebitCard : PaymentMethod<DebitCardPaymentMethodValidator>
{
    public DebitCard(Guid? id, decimal amount, DateOnly expiration, string holderName, string number, string securityNumber)
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

    public static implicit operator DebitCard(Dto.DebitCard card)
        => new(card.Id, card.Amount, card.Expiration, card.Number, card.HolderName, card.SecurityNumber);

    public static implicit operator Dto.DebitCard(DebitCard card)
        => new(card.Id, card.Amount, card.Expiration, card.Number, card.HolderName, card.SecurityNumber);
}