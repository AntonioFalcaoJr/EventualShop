using Contracts.DataTransferObjects;

namespace Domain.ValueObject;

public record Sms(string Number) : INotificationOption
{
    public static implicit operator Sms(string number)
        => new(number);

    public static implicit operator Sms(Dto.Sms sms)
        => new(sms.Number);
}