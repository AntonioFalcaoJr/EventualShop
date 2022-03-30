namespace Domain.Entities.PaymentMethods.CreditCards;

public class CreditCardPaymentMethod : PaymentMethod<CreditCardPaymentMethodValidator>
{
    public DateOnly Expiration { get; init; }
    public string HolderName { get; init; }
    public string Number { get; init; }
    public string SecurityNumber { get; init; }
}