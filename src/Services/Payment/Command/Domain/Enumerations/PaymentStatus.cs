using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace Domain.Enumerations;

public sealed class PaymentStatus : SmartEnum<PaymentStatus>
{
    [JsonConstructor]
    private PaymentStatus(string name, int value)
        : base(name, value) { }

    public static readonly PaymentStatus Ready = new(nameof(Ready), 1);
    public static readonly PaymentStatus Completed = new(nameof(Completed), 2);
    public static readonly PaymentStatus NotCompleted = new(nameof(NotCompleted), 3);
    public static readonly PaymentStatus Cancelled = new(nameof(Cancelled), 4);
    public static readonly PaymentStatus Refunded = new(nameof(Refunded), 5);

    public static implicit operator PaymentStatus(string name)
        => FromName(name);

    public static implicit operator PaymentStatus(int value)
        => FromValue(value);

    public static implicit operator string(PaymentStatus status)
        => status.Name;

    public static implicit operator int(PaymentStatus status)
        => status.Value;
}