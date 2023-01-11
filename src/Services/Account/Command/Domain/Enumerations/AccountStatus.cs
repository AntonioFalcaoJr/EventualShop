using Ardalis.SmartEnum;

namespace Domain.Enumerations;

public class AccountStatus : SmartEnum<AccountStatus>
{
    private AccountStatus(string name, int value)
        : base(name, value) { }

    public static readonly AccountStatus Pending = new PendingStatus();
    public static readonly AccountStatus Active = new ActiveStatus();
    public static readonly AccountStatus Inactive = new InactiveStatus();
    public static readonly AccountStatus Closed = new ClosedStatus();

    public static implicit operator AccountStatus(string name)
        => FromName(name);

    public static implicit operator AccountStatus(int value)
        => FromValue(value);

    public static implicit operator string(AccountStatus status)
        => status.Name;

    public static implicit operator int(AccountStatus status)
        => status.Value;

    public class PendingStatus : AccountStatus
    {
        public PendingStatus()
            : base(nameof(Pending), 1) { }
    }

    public class ActiveStatus : AccountStatus
    {
        public ActiveStatus()
            : base(nameof(Active), 2) { }
    }

    public class InactiveStatus : AccountStatus
    {
        public InactiveStatus()
            : base(nameof(Inactive), 3) { }
    }

    public class ClosedStatus : AccountStatus
    {
        public ClosedStatus()
            : base(nameof(Closed), 4) { }
    }
}