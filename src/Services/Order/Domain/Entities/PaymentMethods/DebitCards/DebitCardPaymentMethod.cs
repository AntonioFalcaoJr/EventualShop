namespace Domain.Entities.PaymentMethods.DebitCards;

public class DebitCardPaymentMethod : PaymentMethod
{
    public DateOnly Expiration { get; init; }
    public string HolderName { get; init; }
    public string Number { get; init; }
    public string SecurityNumber { get; init; }

    protected override bool Validate()
        => OnValidate<DebitCardPaymentMethodValidator, DebitCardPaymentMethod>();
}