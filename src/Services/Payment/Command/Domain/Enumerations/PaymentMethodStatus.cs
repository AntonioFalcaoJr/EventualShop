using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace Domain.Enumerations;

public class PaymentMethodStatus : SmartEnum<PaymentMethodStatus>
{
    [JsonConstructor]
    private PaymentMethodStatus(string name, int value)
        : base(name, value) { }

    public static readonly PaymentMethodStatus Pending = new PendingStatus();
    public static readonly PaymentMethodStatus Authorized = new AuthorizedStatus();
    public static readonly PaymentMethodStatus Cancelled = new CanceledStatus();
    public static readonly PaymentMethodStatus CancellationDenied = new CancellationDeniedStatus();
    public static readonly PaymentMethodStatus Denied = new DeniedStatus();
    public static readonly PaymentMethodStatus RefundDenied = new RefundDeniedStatus();
    public static readonly PaymentMethodStatus Refunded = new RefundedStatus();

    public static implicit operator PaymentMethodStatus(string name)
        => FromName(name);

    public static implicit operator PaymentMethodStatus(int value)
        => FromValue(value);

    public static implicit operator string(PaymentMethodStatus status)
        => status.Name;

    public static implicit operator int(PaymentMethodStatus status)
        => status.Value;

    public class PendingStatus() : PaymentMethodStatus(nameof(Pending), 1);

    public class AuthorizedStatus() : PaymentMethodStatus(nameof(Authorized), 2);

    public class CanceledStatus() : PaymentMethodStatus(nameof(Cancelled), 3);

    public class CancellationDeniedStatus() : PaymentMethodStatus(nameof(CancellationDenied), 4);

    public class DeniedStatus() : PaymentMethodStatus(nameof(Denied), 5);

    public class RefundDeniedStatus() : PaymentMethodStatus(nameof(RefundDenied), 6);

    public class RefundedStatus() : PaymentMethodStatus(nameof(Refunded), 7);
}