using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace Domain.Enumerations;

[method: JsonConstructor]
public class EmailStatus(string name, int value) : SmartEnum<EmailStatus>(name, value)
{
    public static readonly EmailStatus Unverified = new UnverifiedStatus();
    public static readonly EmailStatus Verified = new VerifiedStatus();
    public static readonly EmailStatus Expired = new ExpiredStatus();

    public static implicit operator EmailStatus(string name)
        => FromName(name);

    public static implicit operator EmailStatus(int value)
        => FromValue(value);

    public static implicit operator string(EmailStatus status)
        => status.Name;

    public static implicit operator int(EmailStatus status)
        => status.Value;

    public class UnverifiedStatus() : EmailStatus(nameof(Unverified), 1);

    public class VerifiedStatus() : EmailStatus(nameof(Verified), 2);

    public class ExpiredStatus() : EmailStatus(nameof(Expired), 3);
}