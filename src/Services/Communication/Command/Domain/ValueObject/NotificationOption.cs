namespace Domain.ValueObject;

public abstract record NotificationOption;

public sealed record Email(string Address) : NotificationOption
{
    public static implicit operator Email(string email)
        => new(email);
}

public sealed record Sms(string Number) : NotificationOption
{
    public static implicit operator Sms(string number)
        => new(number);
}

public sealed record Push(Guid DeviceId) : NotificationOption
{
    public static implicit operator Push(Guid deviceId)
        => new(deviceId);
}
