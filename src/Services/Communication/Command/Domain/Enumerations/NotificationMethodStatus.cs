using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace Domain.Enumerations;

public class NotificationMethodStatus : SmartEnum<NotificationMethodStatus>
{
    [JsonConstructor]
    private NotificationMethodStatus(string name, int value)
        : base(name, value) { }

    public static readonly NotificationMethodStatus Pending = new(nameof(Pending), 1);
    public static readonly NotificationMethodStatus Sent = new(nameof(Sent), 2);
    public static readonly NotificationMethodStatus Canceled = new(nameof(Canceled), 3);
    public static readonly NotificationMethodStatus Failed = new(nameof(Failed), 4);

    public static implicit operator NotificationMethodStatus(string name)
        => FromName(name);

    public static implicit operator NotificationMethodStatus(int value)
        => FromValue(value);

    public static implicit operator string(NotificationMethodStatus status)
        => status.Name;

    public static implicit operator int(NotificationMethodStatus status)
        => status.Value;
}