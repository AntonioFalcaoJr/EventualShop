using Contracts.Abstractions;
using Contracts.DataTransferObjects;
using Contracts.Services.Communication.Protobuf;

namespace Contracts.Services.Communication;

public static class Projection
{
    public record NotificationDetails(Guid Id, IEnumerable<Dto.NotificationMethod> Methods, bool IsDeleted) : IProjection
    {
        public static implicit operator Notification(NotificationDetails notification)
            => new()
            {
                Id = notification.Id.ToString(),
                Methods = { notification.Methods.Select(method => (NotificationMethod)method) }
            };
    }
}