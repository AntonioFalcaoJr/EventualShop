using Contracts.DataTransferObjects;

namespace Domain.ValueObjects;

public record Email(string Address, string Subject, string Body) : INotificationOption
{
    public static implicit operator Email(Dto.Email email)
        => new(email.Address, email.Subject, email.Body);
}