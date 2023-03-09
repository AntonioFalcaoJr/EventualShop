using Ardalis.SmartEnum;

namespace Domain.Enumerations;

public class OrderStatus : SmartEnum<OrderStatus>
{
    public OrderStatus(string name, int value) 
        : base(name, value) { }
    
    public static readonly OrderStatus Empty = new(nameof(Empty), 0);
    public static readonly OrderStatus Confirmed = new(nameof(Confirmed), 1);
    public static readonly OrderStatus PendingPayment = new(nameof(PendingPayment), 2);
    public static readonly OrderStatus Failed = new(nameof(Failed), 3);
    public static readonly OrderStatus Processing = new(nameof(Processing), 4);
    public static readonly OrderStatus Completed = new(nameof(Completed), 5);
    public static readonly OrderStatus OnHold = new(nameof(OnHold), 6);
    public static readonly OrderStatus Canceled = new(nameof(Canceled), 7);
    public static readonly OrderStatus Refunded = new(nameof(Refunded), 8);

    public static implicit operator OrderStatus(string name)
        => FromName(name);

    public static implicit operator OrderStatus(int value)
        => FromValue(value);

    public static implicit operator string(OrderStatus status)
        => status.Name;

    public static implicit operator int(OrderStatus status)
        => status.Value;
}