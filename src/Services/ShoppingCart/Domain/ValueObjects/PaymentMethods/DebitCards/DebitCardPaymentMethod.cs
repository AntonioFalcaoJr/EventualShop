namespace Domain.ValueObjects.PaymentMethods.DebitCards;

public record DebitCardPaymentMethod(decimal Amount, DateOnly Expiration, string HolderName, string Number, string SecurityNumber) : PaymentMethod<DebitCardPaymentMethodValidator>(Amount) { }