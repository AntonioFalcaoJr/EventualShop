using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace Domain.Enumerations;

public class PaymentMethodStatus : SmartEnum<PaymentMethodStatus>
{
    [JsonConstructor]
    private PaymentMethodStatus(string name, int value)
        : base(name, value) { }

    public static readonly PaymentMethodStatus Authorized = new AuthorizedMethodStatus();
    public static readonly PaymentMethodStatus Canceled = new CanceledMethodStatus();
    public static readonly PaymentMethodStatus CancellationDenied = new CancellationDeniedMethodStatus();
    public static readonly PaymentMethodStatus Denied = new DeniedMethodStatus();
    public static readonly PaymentMethodStatus Ready = new ReadyMethodStatus();
    public static readonly PaymentMethodStatus RefundDenied = new RefundDeniedMethodtatus();
    public static readonly PaymentMethodStatus Refunded = new RefundedMethodtatus();

    public static implicit operator PaymentMethodStatus(string name)
        => FromName(name);

    public static implicit operator PaymentMethodStatus(int value)
        => FromValue(value);

    public static implicit operator string(PaymentMethodStatus status)
        => status.Name;

    public static implicit operator int(PaymentMethodStatus status)
        => status.Value;


    public class AuthorizedStatus : PaymentMethodStatus
    {
        public AuthorizedMethodStatus() : base(nameof(Authorized), 1) { }
    }
    
    public class CanceledStatus : PaymentMethodStatus
    {
        public CanceledMethodStatus() : base(nameof(Canceled), 2) { }
    }
    
    public class CancellationDeniedStatus : PaymentMethodStatus
    {
        public CancellationDeniedMethodStatus() : base(nameof(CancellationDenied), 3) { }
    }
    
    public class DeniedStatus : PaymentMethodStatus
    {
        public DeniedMethodStatus() : base(nameof(Denied), 4) { }
    }
    
    public class ReadyStatus : PaymentMethodStatus
    {
        public ReadyMethodStatus() : base(nameof(Ready), 5) { }
    }
    
    public class RefundDeniedStatus : PaymentMethodStatus
    {
        public RefundDeniedMethodtatus() : base(nameof(RefundDenied), 6) { }
    }
    
    public class RefundedStatus : PaymentMethodStatus
    {
        public RefundedMethodtatus() : base(nameof(Refunded), 7) { }
    }
}