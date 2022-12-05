namespace Domain.ValueObject;

public sealed record Email(string Address) : NotificationOption
{
    public static implicit operator Email(string email)
        => new(email);
}