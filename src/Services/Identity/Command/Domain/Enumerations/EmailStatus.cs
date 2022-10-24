using System.Text.Json.Serialization;
using Ardalis.SmartEnum;

namespace Domain.Enumerations;

public class EmailStatus : SmartEnum<EmailStatus>
{
    [JsonConstructor]
    private EmailStatus(string name, int value)
        : base(name, value) { }

    public static readonly EmailStatus Unverified = new(nameof(Unverified), 1);
    public static readonly EmailStatus Verified = new(nameof(Verified), 2);
    public static readonly EmailStatus Expired = new(nameof(Expired), 3);

    public static implicit operator EmailStatus(string name)
        => FromName(name);

    public static implicit operator EmailStatus(int value)
        => FromValue(value);

    public static implicit operator string(EmailStatus status)
        => status.Name;

    public static implicit operator int(EmailStatus status)
        => status.Value;
}