namespace Domain.ValueObjects.PaymentMethods;

public interface IPaymentMethod
{
    public static readonly IPaymentMethod Undefined = new UndefinedPaymentMethod();

    public record UndefinedPaymentMethod : IPaymentMethod;
}