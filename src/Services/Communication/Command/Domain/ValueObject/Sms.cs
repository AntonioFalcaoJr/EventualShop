namespace Domain.ValueObject;

public sealed record Sms(string Number) : NotificationOption
{
    public static implicit operator Sms(string number)
        => new(number);
}