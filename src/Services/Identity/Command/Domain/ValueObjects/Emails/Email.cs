using Domain.Enumerations;

namespace Domain.ValueObjects.Emails;

public record Email(string Address, DateTimeOffset ExpirationTime, EmailStatus Status)
{
    private Email(string address, DateTimeOffset expirationTime)
        : this(address, expirationTime, EmailStatus.Unverified) { }

    private Email(string address)
        : this(address, DateTimeOffset.Now.AddMinutes(15)) { }

    public bool IsVerified
        => Status == EmailStatus.Verified;

    public bool IsExpired
        => DateTimeOffset.Now > ExpirationTime;

    public static implicit operator Email(string address)
        => new(address);
}