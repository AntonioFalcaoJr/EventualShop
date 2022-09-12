using Domain.Enumerations;

namespace Domain.ValueObjects.Emails;

public record Email(string Address, EmailStatus Status)
{
    public static implicit operator Email(string address) 
        => new(address, EmailStatus.Unverified);
}