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

    public class PendingStatus : PaymentMethodStatus
    {
        public PendingStatus()
            : base(nameof(Pending), 1) { }
    }

    public class AuthorizedStatus : PaymentMethodStatus
    {
        public AuthorizedStatus() 
            : base(nameof(Authorized), 2) { }
    }

    public class CanceledStatus : PaymentMethodStatus
    {
        public CanceledStatus() 
            : base(nameof(Cancelled), 3) { }
    }

    public class CancellationDeniedStatus : PaymentMethodStatus
    {
        public CancellationDeniedStatus() 
            : base(nameof(CancellationDenied), 4) { }
    }

    public class DeniedStatus : PaymentMethodStatus
    {
        public DeniedStatus() 
            : base(nameof(Denied), 5) { }
    }

    public class RefundDeniedStatus : PaymentMethodStatus
    {
        public RefundDeniedStatus() 
            : base(nameof(RefundDenied), 6) { }
    }

    public class RefundedStatus : PaymentMethodStatus
    {
        public RefundedStatus() 
            : base(nameof(Refunded), 7) { }
    }
}