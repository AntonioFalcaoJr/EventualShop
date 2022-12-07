using Contracts.DataTransferObjects;

namespace Domain.ValueObject;

public record struct PushMobile(Guid DeviceId) : INotificationOption
{
    public static implicit operator PushMobile(Guid deviceId)
        => new(deviceId);

    public static implicit operator PushMobile(Dto.PushMobile push)
        => new(push.DeviceId);
}