namespace Domain.Entities.PaymentMethods.DebitCards;

public class DebitCardPaymentMethod : PaymentMethod<DebitCardPaymentMethodValidator>
{
    public DateOnly Expiration { get; init; }
    public string HolderName { get; init; }
    public string Number { get; init; }
    public string SecurityNumber { get; init; }
}