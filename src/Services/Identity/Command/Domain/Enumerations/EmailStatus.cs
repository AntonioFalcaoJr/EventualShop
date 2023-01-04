using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace Domain.Enumerations;

public class EmailStatus : SmartEnum<EmailStatus>
{
    [JsonConstructor]
    public EmailStatus(string name, int value)
        : base(name, value) { }

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

    public class UnverifiedStatus : EmailStatus
    {
        public UnverifiedStatus() 
            : base(nameof(Unverified), 1) { }
    }
    
    public class VerifiedStatus : EmailStatus
    {
        public VerifiedStatus() 
            : base(nameof(Verified), 2) { }
    }
    
    public class ExpiredStatus : EmailStatus
    {
        public ExpiredStatus() 
            : base(nameof(Expired), 3) { }
    }
}