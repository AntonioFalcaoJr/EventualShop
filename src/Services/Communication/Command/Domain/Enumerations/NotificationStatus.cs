using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace Domain.Enumerations;

public class NotificationStatus : SmartEnum<NotificationStatus>
{
    [JsonConstructor]
    private NotificationStatus(string name, int value)
        : base(name, value) { }

    public static readonly NotificationStatus Active = new(nameof(Active), 1);
    public static readonly NotificationStatus Completed = new(nameof(Completed), 2);
    public static readonly NotificationStatus Failed = new(nameof(Failed), 3);
    public static readonly NotificationStatus Canceled = new(nameof(Canceled), 4);

    public static implicit operator NotificationStatus(string name)
        => FromName(name);

    public static implicit operator NotificationStatus(int value)
        => FromValue(value);

    public static implicit operator string(NotificationStatus status)
        => status.Name;

    public static implicit operator int(NotificationStatus status)
        => status.Value;
}