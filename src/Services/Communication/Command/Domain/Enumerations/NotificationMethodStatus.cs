using System.Text.Json.Serialization;
using Ardalis.SmartEnum;

namespace Domain.Enumerations;

public class NotificationMethodStatus : SmartEnum<NotificationMethodStatus>
{
    [JsonConstructor]
    private NotificationMethodStatus(string name, int value)
        : base(name, value) { }

    public static readonly NotificationMethodStatus Ready = new(nameof(Ready), 1);
    public static readonly NotificationMethodStatus Completed = new(nameof(Completed), 2);
    public static readonly NotificationMethodStatus Failed = new(nameof(Failed), 3);
    public static readonly NotificationMethodStatus Canceled = new(nameof(Canceled), 4);
}