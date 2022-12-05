namespace Domain.ValueObject;

public sealed record Push(Guid DeviceId) : NotificationOption
{
    public static implicit operator Push(Guid deviceId)
        => new(deviceId);
}