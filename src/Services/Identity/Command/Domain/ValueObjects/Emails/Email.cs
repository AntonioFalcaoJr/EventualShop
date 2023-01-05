using Domain.Enumerations;

namespace Domain.ValueObjects.Emails;

public record Email(string Address, EmailStatus Status)
{
    private Email(string address)
        : this(address, EmailStatus.Unverified) { }

    public static implicit operator Email(string address)
        => new(address);
}