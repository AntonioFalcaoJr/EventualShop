using Contracts.DataTransferObjects;

namespace Domain.ValueObject;

public record struct Email(string Address) : INotificationOption
{
    public static implicit operator Email(string address)
        => new(address);

    public static implicit operator Email(Dto.Email email)
        => new(email.Address);
}