using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace Domain.Enumerations;

public class NotificationMethodStatus : SmartEnum<NotificationMethodStatus>
{
    [JsonConstructor]
    private NotificationMethodStatus(string name, int value)
        : base(name, value) { }

    public static readonly NotificationMethodStatus Pending = new PendingStatus();
    public static readonly NotificationMethodStatus Cancelled = new CancelledStatus();
    public static readonly NotificationMethodStatus Sent = new SentStatus();
    public static readonly NotificationMethodStatus Failed = new FailedStatus();

    public static implicit operator NotificationMethodStatus(string name)
        => FromName(name);

    public static implicit operator NotificationMethodStatus(int value)
        => FromValue(value);

    public static implicit operator string(NotificationMethodStatus status)
        => status.Name;

    public static implicit operator int(NotificationMethodStatus status)
        => status.Value;

    public class PendingStatus() : NotificationMethodStatus(nameof(Pending), 1);

    public class CancelledStatus() : NotificationMethodStatus(nameof(Cancelled), 2);

    public class SentStatus() : NotificationMethodStatus(nameof(Sent), 3);

    public class FailedStatus() : NotificationMethodStatus(nameof(Failed), 4);
}