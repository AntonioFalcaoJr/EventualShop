using Contracts.DataTransferObjects;

namespace Domain.ValueObject;

public record PushWeb(Guid UserId) : INotificationOption
{
    public static implicit operator PushWeb(Guid userId)
        => new(userId);

    public static implicit operator PushWeb(Dto.PushWeb push)
        => new(push.UserId);
}