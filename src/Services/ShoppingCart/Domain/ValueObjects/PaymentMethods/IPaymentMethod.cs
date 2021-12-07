namespace Domain.ValueObjects.PaymentMethods;

public interface IPaymentMethod
{
    decimal Amount { get; }
}