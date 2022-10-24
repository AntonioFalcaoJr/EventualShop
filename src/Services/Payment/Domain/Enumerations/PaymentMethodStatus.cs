using System.Text.Json.Serialization;
using Ardalis.SmartEnum;

namespace Domain.Enumerations;

public sealed class PaymentMethodStatus : SmartEnum<PaymentMethodStatus>
{
    [JsonConstructor]
    private PaymentMethodStatus(string name, int value)
        : base(name, value) { }

    public static readonly PaymentMethodStatus Authorized = new(nameof(Authorized), 1);
    public static readonly PaymentMethodStatus Canceled = new(nameof(Canceled), 2);
    public static readonly PaymentMethodStatus CancellationDenied = new(nameof(CancellationDenied), 3);
    public static readonly PaymentMethodStatus Denied = new(nameof(Denied), 4);
    public static readonly PaymentMethodStatus Ready = new(nameof(Ready), 5);
    public static readonly PaymentMethodStatus RefundDenied = new(nameof(RefundDenied), 6);
    public static readonly PaymentMethodStatus Refunded = new(nameof(Refunded), 7);

    public static implicit operator PaymentMethodStatus(string name)
        => FromName(name);

    public static implicit operator PaymentMethodStatus(int value)
        => FromValue(value);

    public static implicit operator string(PaymentMethodStatus status)
        => status.Name;

    public static implicit operator int(PaymentMethodStatus status)
        => status.Value;
}