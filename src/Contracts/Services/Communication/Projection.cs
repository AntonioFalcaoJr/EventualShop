using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Communication;

public static class Projection
{
    public record NotificationDetails(Guid Id, bool IsDeleted, long Version) : IProjection
    {
        public static implicit operator Protobuf.NotificationDetails(NotificationDetails notification)
            => new() { NotificationId = notification.Id.ToString() };
    }

    public record NotificationMethodDetails(Guid Id, Guid NotificationId, Dto.INotificationOption Option, bool IsDeleted, long Version) : IProjection
    {
        public static implicit operator Protobuf.NotificationMethodDetails(NotificationMethodDetails method)
            => new()
            {
                MethodId = method.Id.ToString(),
                NotificationId = method.NotificationId.ToString(),
                Option = method.Option switch
                {
                    Dto.Email email => new() { Email = email },
                    Dto.Sms sms => new() { Sms = sms },
                    Dto.PushMobile pushMobile => new() { PushMobile = pushMobile },
                    Dto.PushWeb pushWeb => new() { PushWeb = pushWeb },
                    _ => default
                }
            };
    }
}