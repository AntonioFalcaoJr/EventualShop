using Ardalis.SmartEnum;

namespace Domain.Enumerations;

public class CartStatus : SmartEnum<CartStatus>
{
    private CartStatus(string name, int value)
        : base(name, value) { }

    public static readonly CartStatus Empty = new EmptyStatus();
    public static readonly CartStatus Active = new ActiveStatus();
    public static readonly CartStatus Abandoned = new AbandonedStatus();
    public static readonly CartStatus CheckedOut = new CheckedOutStatus();

    public static implicit operator CartStatus(string name)
        => FromName(name);

    public static implicit operator CartStatus(int value)
        => FromValue(value);

    public static implicit operator string(CartStatus status)
        => status.Name;

    public static implicit operator int(CartStatus status)
        => status.Value;

    public class EmptyStatus : CartStatus
    {
        public EmptyStatus()
            : base(nameof(Empty), 0) { }
    }

    public class ActiveStatus : CartStatus
    {
        public ActiveStatus()
            : base(nameof(Active), 1) { }
    }

    public class AbandonedStatus : CartStatus
    {
        public AbandonedStatus()
            : base(nameof(Abandoned), 2) { }
    }

    public class CheckedOutStatus : CartStatus
    {
        public CheckedOutStatus()
            : base(nameof(CheckedOut), 3) { }
    }
}