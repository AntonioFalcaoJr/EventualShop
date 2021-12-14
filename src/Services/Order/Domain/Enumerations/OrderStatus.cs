namespace Domain.Enumerations;

public enum OrderStatus
{
    Confirmed,
    PendingPayment,
    Failed,
    Processing,
    Completed,
    OnHold,
    Canceled,
    Refunded,
    AuthenticationRequired
}